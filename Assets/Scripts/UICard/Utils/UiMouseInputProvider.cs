using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{
	public enum DragDirection
	{
		None,
		Down,
		Left,
		Top,
		Right
	}

	// Interface for all the Unity Mouse Input System.
	public interface IMouseInput
		:
		IPointerClickHandler,
		IBeginDragHandler,
		IDragHandler,
		IEndDragHandler,
		IDropHandler,
		IPointerDownHandler,
		IPointerUpHandler,
		IPointerEnterHandler,
		IPointerExitHandler
	{
		//clicks
		Action<PointerEventData> onPointerClick { get; set; }
		Action<PointerEventData> onPointerDown { get; set; }
		Action<PointerEventData> onPointerUp { get; set; }

		//drag
		Action<PointerEventData> onBeginDrag { get; set; }
		Action<PointerEventData> onDrag { get; set; }
		Action<PointerEventData> onEndDrag { get; set; }
		Action<PointerEventData> onDrop { get; set; }

		//enter
		Action<PointerEventData> onPointerEnter { get; set; }
		Action<PointerEventData> onPointerExit { get; set; }

		Vector2 MousePosition { get; }
		DragDirection DragDirection { get; }
	}



	// Wrap of all the Unity Input System into a Monobehavior.
	[RequireComponent(typeof(Collider))]
	public class UiMouseInputProvider : MonoBehaviour, IMouseInput
	{
		private Vector3 _oldDragPosition;

		public DragDirection DragDirection
		{
			get { return GetDragDirection(); }
		}
		Vector2 IMouseInput.MousePosition
		{
			get { return Input.mousePosition; }
		}

		#region Delegates 

		public Action<PointerEventData> onPointerDown { get; set; }
		public Action<PointerEventData> onPointerUp { get; set; }
		public Action<PointerEventData> onPointerClick { get; set; }
		public Action<PointerEventData> onBeginDrag { get; set; }
		public Action<PointerEventData> onDrag { get; set; }
		public Action<PointerEventData> onEndDrag { get; set; }
		public Action<PointerEventData> onDrop { get; set; }
		public Action<PointerEventData> onPointerEnter { get; set; }
		public Action<PointerEventData> onPointerExit { get; set; }

		#endregion


		private void Awake()
		{
			// Currently using PhysicsRaycaster, but can be also considered PhysicsRaycaster2D.
			if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
			{
				throw new Exception(GetType() + " needs an " + typeof(PhysicsRaycaster) + " on the MainCamera");
			}
		}

		// While dragging returns the direction of the movement.
		private DragDirection GetDragDirection()
		{
			Vector3 currentPosition = Input.mousePosition;
			Vector3 normalized = (currentPosition - _oldDragPosition).normalized;
			_oldDragPosition = currentPosition;

			if (normalized.x > 0)
				return DragDirection.Right;

			if (normalized.x < 0)
				return DragDirection.Left;

			if (normalized.y > 0)
				return DragDirection.Top;

			return normalized.y < 0 ? DragDirection.Down : DragDirection.None;
		}




		#region Unity Mouse Events

		public void OnBeginDrag(PointerEventData eventData)
		{
			onBeginDrag?.Invoke(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			onDrag?.Invoke(eventData);
		}

		public void OnDrop(PointerEventData eventData)
		{
			onDrop?.Invoke(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			onEndDrag?.Invoke(eventData);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			onPointerClick?.Invoke(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			onPointerDown?.Invoke(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			onPointerUp?.Invoke(eventData);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			onPointerEnter?.Invoke(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			onPointerExit?.Invoke(eventData);
		}

		#endregion

	}
}