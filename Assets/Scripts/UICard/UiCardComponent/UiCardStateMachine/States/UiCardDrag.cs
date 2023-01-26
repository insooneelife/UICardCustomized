using Extensions;
using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{
	public class UiCardDrag : UiBaseCardState
	{
		private Vector3 _startEuler;

		private Camera _camera;

		public UiCardDrag(IUiCard handler, Camera camera, BaseStateMachine fsm, UiCardParameters parameters)
			:
			base(handler, fsm, parameters)
		{
			_camera = camera;
		}	


		private Vector3 WorldPoint()
		{
			Vector2 mousePosition = _handler.Input.MousePosition;
			Vector3 worldPoint = _camera.ScreenToWorldPoint(mousePosition);
			return worldPoint;
		}

		private void FollowCursor()
		{
			float myZ = _handler.transform.position.z;
			_handler.transform.position = WorldPoint().WithZ(myZ);
		}

		
		#region FSM

		public override void OnUpdate()
		{
			if (_handler.CardHowToUse == EnumTypes.CardHowToUses.TargetGround)
			{
			}
			else
			{
				FollowCursor();
			}
		}

		public override void OnEnterState()
		{
			//stop any movement
			_handler.Movement.StopMotion();

			//cache old values
			_startEuler = _handler.transform.eulerAngles;

			_handler.RotateTo(Vector3.zero, Parameters.RotationSpeed);
			MakeRenderFirst();
			RemoveAllTransparency();
		}

		public override void OnExitState()
		{
			//reset position and rotation
			if (_handler.transform)
			{
				_handler.RotateTo(_startEuler, Parameters.RotationSpeed);
				MakeRenderNormal();
			}
		}

		#endregion
		
	}
}