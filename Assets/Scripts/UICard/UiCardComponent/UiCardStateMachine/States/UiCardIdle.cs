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
			_handler.Input.onPointerDown += OnPointerDown;
			_handler.Input.onPointerEnter += OnPointerEnter;

			if (_handler.Movement.IsOperating)
			{
				_handler.DisableCollision();
				_handler.Movement.onFinishMotion += OnFinishMotion;
			}
			else
			{
				Enable();
			}

			_handler.MakeRenderNormal();
			_handler.ScaleTo(_defaultSize, Parameters.ScaleSpeed);
		}

		public override void OnExitState()
		{
			_handler.Input.onPointerDown -= OnPointerDown;
			_handler.Input.onPointerEnter -= OnPointerEnter;
			_handler.Movement.onFinishMotion -= OnFinishMotion;
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