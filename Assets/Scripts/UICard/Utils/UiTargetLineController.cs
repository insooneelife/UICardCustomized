using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnumTypes
{
	public enum CardHowToUses
	{
		Normal, TargetGround
	}
}

namespace UICard
{
	

	public class UiTargetLineController : MonoBehaviour
	{
		[SerializeField]
		private LineRenderer _line;

		private Camera _camera;

		private IUiCard _card;


		public void Init(IUiCard card)
		{
			_card = card;			
			_camera = card.MainCamera;
			_line = gameObject.GetComponent<LineRenderer>();

			_line.positionCount = 2;
			_line.useWorldSpace = true;
			_line.startWidth = 0.2f;
			_line.endWidth = 0.2f;
			_line.startColor = Color.white;
			_line.endColor = Color.white;

			AddEventHandlers();
		}

		private void OnDisable()
		{
			RemoveEventHandlers();
			_card = null;
		}


		public void Show()
		{
			_line.enabled = true;
		}

		public void Hide()
		{
			_line.enabled = false;
			DrawLine(Vector3.zero, Vector3.zero);
		}


		private void DrawLine(Vector3 wFrom, Vector3 wTo)
		{
			wFrom.z = 0;
			wTo.z = 0;
			
			_line.SetPosition(0, wFrom);
			_line.SetPosition(1, wTo);
		}


		private void OnBeginDrag(PointerEventData edata)
		{
			if (_card.CardHowToUse == EnumTypes.CardHowToUses.TargetGround)
			{
				Show();
				Vector3 wFrom = _card.transform.position;
				Vector3 wTo = _camera.ScreenToWorldPoint(edata.position);
				DrawLine(wFrom, wTo);

				
			}
		}

		private void OnDrag(PointerEventData edata)
		{
			if (_card.CardHowToUse == EnumTypes.CardHowToUses.TargetGround)
			{
				Vector3 wFrom = _card.transform.position;
				Vector3 wTo = _camera.ScreenToWorldPoint(edata.position);
				DrawLine(wFrom, wTo);
			}
		}

		private void OnEndDrag(PointerEventData edata)
		{
			if (_card.CardHowToUse == EnumTypes.CardHowToUses.TargetGround)
			{
				Hide();
			}
		}
				

		private void AddEventHandlers()
		{
			if (_card != null)
			{ 
				_card.Input.onBeginDrag += OnBeginDrag;
				_card.Input.onEndDrag += OnEndDrag;
				_card.Input.onDrag += OnDrag;
			}
		}

		private void RemoveEventHandlers()
		{
			if (_card != null)
			{
				_card.Input.onBeginDrag += OnBeginDrag;
				_card.Input.onEndDrag += OnEndDrag;
				_card.Input.onDrag += OnDrag;
			}
		}
	}

}

