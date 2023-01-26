using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.Playables;

namespace UICard
{

	// State Machine that holds all states of a UI Card.
	public class UiCardHandFsm : BaseStateMachine
	{
		private UiCardParameters _cardConfigsParameters;

		private UiCardIdle _idleState;
		private UiCardDisable _disableState;
		private UiCardDrag _dragState;
		private UiCardHover _hoverState;
		private UiCardDraw _drawState;
		private UiCardDiscard _discardState;
		private UiCardPlay _playState;
		
		public UiCardHandFsm(Camera camera, UiCardParameters cardConfigsParameters, IUiCard handler = null) :
			base(handler)
		{
			_cardConfigsParameters = cardConfigsParameters;

			_idleState = new UiCardIdle(handler, this, _cardConfigsParameters);
			_disableState = new UiCardDisable(handler, this, _cardConfigsParameters);
			_dragState = new UiCardDrag(handler, camera, this, _cardConfigsParameters);
			_hoverState = new UiCardHover(handler, this, _cardConfigsParameters);
			_drawState = new UiCardDraw(handler, this, _cardConfigsParameters);
			_discardState = new UiCardDiscard(handler, this, _cardConfigsParameters);
			_playState = new UiCardPlay(handler, this, _cardConfigsParameters);
			
			RegisterState(_idleState);
			RegisterState(_disableState);
			RegisterState(_dragState);
			RegisterState(_hoverState);
			RegisterState(_drawState);
			RegisterState(_discardState);
			RegisterState(_playState);

			Initialize();
		}
		
		public void Hover()
		{
			PushState<UiCardHover>();
		}

		public void Disable()
		{
			PushState<UiCardDisable>();
		}

		public void Enable()
		{
			PushState<UiCardIdle>();
		}

		public void Select()
		{
			PushState<UiCardDrag>();
		}


		public void Unselect()
		{
			Enable();
		}

		public void Draw()
		{
			PushState<UiCardDraw>();
		}

		public void Discard()
		{
			PushState<UiCardDiscard>();
		}

		public void Play()
		{
			PushState<UiCardPlay>();
		}	

	}
}