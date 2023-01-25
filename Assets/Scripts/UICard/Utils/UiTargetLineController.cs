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

namespace Tools.UI.Card
{
	

	public class UiTargetLineController : MonoBehaviour
	{
		[SerializeField]
		private LineRenderer _line;

		private Camera _camera;

		private IUiCard _card;


		public void Awake()
		{
			IUiCard card = GetComponent<IUiCard>();

			_card = card;			
			_camera = card.MainCamera;
			_line = gameObject.GetComponent<LineRenderer>();

			_line.positionCount = 2;
			_line.useWorldSpace = true;
			_line.startWidth = 0.2f;
			_line.endWidth = 0.2f;
			_line.startColor = Color.white;
			_line.endColor = Color.white;

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

		private void OnEnable()
		{
			AddEventHandlers();
		}


		private void OnDisable()
		{
			RemoveEventHandlers();
		}

		private void AddEventHandlers()
		{
			_card.Input.OnBeginDrag += OnBeginDrag;
			_card.Input.OnEndDrag += OnEndDrag;
			_card.Input.OnDrag += OnDrag;
		}

		private void RemoveEventHandlers()
		{
			_card.Input.OnBeginDrag += OnBeginDrag;
			_card.Input.OnEndDrag += OnEndDrag;
			_card.Input.OnDrag += OnDrag;
		}
	}

}

