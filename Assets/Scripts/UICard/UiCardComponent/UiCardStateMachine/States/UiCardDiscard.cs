using Patterns.StateMachine;
using UnityEngine;

namespace UICard
{
	// State when a cards has been discarded.
	public class UiCardDiscard : UiBaseCardState
	{
		public UiCardDiscard(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) 
			:
			base(handler, fsm, parameters)
		{
		}
		
		
		#region FSM

		public override void OnEnterState()
		{
			Disable();
			SetScale();
			SetRotation();
		}

		void SetScale()
		{
			Vector3 finalScale = _handler.transform.localScale * Parameters.DiscardedSize;
			_handler.ScaleTo(finalScale, Parameters.ScaleSpeed);
		}

		void SetRotation()
		{
			float speed = _handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;
			_handler.RotateTo(Vector3.zero, speed);
		}

		#endregion
	}
}