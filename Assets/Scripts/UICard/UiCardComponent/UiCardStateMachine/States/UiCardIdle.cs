using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{

	// UI Card Idle behavior.
	public class UiCardIdle : UiBaseCardState
	{
		private Vector3 _defaultSize;


		public UiCardIdle(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters)
			:
			base(handler, fsm, parameters)
		{
			_defaultSize = _handler.transform.localScale;
		}
		

		#region FSM

		public override void OnEnterState()
		{
			_handler.Input.OnPointerDown += OnPointerDown;
			_handler.Input.OnPointerEnter += OnPointerEnter;

			if (_handler.Movement.IsOperating)
			{
				DisableCollision();
				_handler.Movement.OnFinishMotion += OnFinishMotion;
			}
			else
			{
				Enable();
			}

			MakeRenderNormal();
			_handler.ScaleTo(_defaultSize, Parameters.ScaleSpeed);
		}

		public override void OnExitState()
		{
			_handler.Input.OnPointerDown -= OnPointerDown;
			_handler.Input.OnPointerEnter -= OnPointerEnter;
			_handler.Movement.OnFinishMotion -= OnFinishMotion;
		}

		#endregion FSM


		#region Event Handlers

		private void OnPointerEnter(PointerEventData obj)
		{
			if (_fsm.IsCurrent(this))
			{ 
				_handler.Hover();
			}
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			if (_fsm.IsCurrent(this))
			{ 
				_handler.Select();
			}
		}

		private void OnFinishMotion(IUiCard card)
		{
			Enable();
		}

		#endregion Event Handlers
	}
}