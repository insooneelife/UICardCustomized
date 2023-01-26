using Patterns.StateMachine;
using UnityEngine;

namespace UICard
{
	//	This state draw the collider of the card.
	public class UiCardDraw : UiBaseCardState
	{
		private Vector3 _startScale;

		public UiCardDraw(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) 
			:
			base(handler, fsm, parameters)
		{}
		
		
		

		private void GoToIdle()
		{
			_handler.Enable();
		}

		private void CachePreviousValue()
		{
			_startScale = _handler.transform.localScale;
			_handler.transform.localScale *= Parameters.StartSizeWhenDraw;
		}

		private void SetScale()
		{
			_handler.ScaleTo(_startScale, Parameters.ScaleSpeed);
		}



		#region Events Handlers
		private void OnFinishMotion(IUiCard card)
		{
			GoToIdle();
		}
		#endregion Events Handlers


		#region FSM

		public override void OnEnterState()
		{
			CachePreviousValue();
			_handler.DisableCollision();
			SetScale();
			_handler.Movement.onFinishMotion += OnFinishMotion;
		}

		public override void OnExitState()
		{
			_handler.Movement.onFinishMotion -= OnFinishMotion;
		}
		

		#endregion
	}
}