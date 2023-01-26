using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{
	// Base zones where the user can drop a UI Card.
	public abstract class UiBaseDropZone : MonoBehaviour
	{
		protected IUiPlayerHand _cardHand;
		
		protected virtual void Awake()
		{
			_cardHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
			
			_cardHand.onAddCard += OnAddCard;
			_cardHand.onRemoveCard += OnRemoveCard;
		}

		protected virtual void OnDestroy()
		{
			_cardHand.onAddCard -= OnAddCard;
			_cardHand.onRemoveCard -= OnRemoveCard;
		}

		protected virtual void OnPointerUp(PointerEventData eventData) {}


		private void OnPointerUpInternal(PointerEventData eventData)
		{
			Collider collider = GetComponent<Collider>();
			Vector2 screenPos = eventData.position;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
			Ray ray = new Ray(worldPos, Vector3.forward);
			RaycastHit hitInfo;
			
			if (collider.Raycast(ray, out hitInfo, 10000))
			{
				OnPointerUp(eventData);
			}
		}

		private void OnAddCard(IUiCard card)
		{
			card.Input.onPointerUp += OnPointerUpInternal;
		}

		private void OnRemoveCard(IUiCard card)
		{
			card.Input.onPointerUp -= OnPointerUpInternal;
		}


	}
}