using Extensions;
using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{
	public class UiCardHover : UiBaseCardState
	{
		private Vector3 _startPosition;
		private Vector3 _startEuler;
		private Vector3 _startScale;

		public UiCardHover(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) 
			:
			base(handler, fsm, parameters)
		{}

		
		private void ResetValues()
		{
			float rotationSpeed = _handler.IsPlayer ? _parameters.RotationSpeed : _parameters.RotationSpeedP2;

			if (_handler.CacheBendData != null)
			{
				_handler.RotateTo(_handler.CacheBendData.rotateEuler, rotationSpeed);
				_handler.MoveTo(_handler.CacheBendData.movePosition, _parameters.HoverSpeed);
			}
			else
			{
				_handler.RotateTo(_startEuler, rotationSpeed);
				_handler.MoveTo(_startPosition, _parameters.HoverSpeed);
			}

			_handler.ScaleTo(_startScale, _parameters.ScaleSpeed);
		}

		private void SetRotation()
		{
			if (_parameters.HoverRotation)
				return;

			float speed = _handler.IsPlayer ? _parameters.RotationSpeed : _parameters.RotationSpeedP2;

			_handler.RotateTo(Vector3.zero, speed);
		}


		// View Math.
		private void SetPosition()
		{
			Camera camera = _handler.MainCamera;
			Vector3 halfCardHeight = new Vector3(0, _handler.Bounds.size.y / 2);
			Vector3 bottomEdge = _handler.MainCamera.ScreenToWorldPoint(Vector3.zero);
			Vector3 topEdge = _handler.MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));
			int edgeFactor = _handler.transform.CloserEdge(camera, Screen.width, Screen.height);
			Vector3 myEdge = edgeFactor == 1 ? bottomEdge : topEdge;
			Vector3 edgeY = new Vector3(0, myEdge.y);
			Vector3 currentPosWithoutY = new Vector3(_handler.transform.position.x, 0, _handler.transform.position.z);
			Vector3 hoverHeightParameter = new Vector3(0, _parameters.HoverHeight);
			Vector3 final = currentPosWithoutY + edgeY + (halfCardHeight + hoverHeightParameter) * edgeFactor;
			_handler.MoveTo(final, _parameters.HoverSpeed);
		}

		private void SetScale()
		{
			Vector3 currentScale = _handler.transform.localScale;
			Vector3 finalScale = currentScale * _parameters.HoverScale;
			_handler.ScaleTo(finalScale, _parameters.ScaleSpeed);
		}

		private void CachePreviousValues()
		{
			_startPosition = _handler.transform.position;
			_startEuler = _handler.transform.eulerAngles;
			_startScale = _handler.transform.localScale;
		}

		private void SubscribeInput()
		{
			_handler.Input.onPointerExit += OnPointerExit;
			_handler.Input.onPointerDown += OnPointerDown;
		}

		private void UnsubscribeInput()
		{
			_handler.Input.onPointerExit -= OnPointerExit;
			_handler.Input.onPointerDown -= OnPointerDown;
		}

		private void CalcEdge()
		{
		}

		#region Events Handlers
		private void OnPointerExit(PointerEventData obj)
		{
			if (Fsm.IsCurrent(this))
			{ 
				_handler.Enable();
			}
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			if (Fsm.IsCurrent(this))
			{
				_handler.Select();
			}
		}
		#endregion Events Handlers


		#region FSM

		public override void OnEnterState()
		{
			_handler.MakeRenderFirst();
			SubscribeInput();
			CachePreviousValues();
			SetScale();
			SetPosition();
			SetRotation();
		}

		public override void OnExitState()
		{
			ResetValues();
			UnsubscribeInput();
			_handler.DisableCollision();
		}

		#endregion

		
	}
}