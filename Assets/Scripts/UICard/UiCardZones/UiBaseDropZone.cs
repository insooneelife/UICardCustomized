using UnityEngine;
using UnityEngine.EventSystems;

namespace UICard
{
    /// <summary>
    ///     Base zones where the user can drop a UI Card.
    /// </summary>
    [RequireComponent(typeof(IMouseInput))]
    public abstract class UiBaseDropZone : MonoBehaviour
    {
        protected IUiPlayerHand CardHand { get; set; }
        protected IMouseInput Input { get; set; }

        protected virtual void Awake()
        {
			CardHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerUp += OnPointerUp;

			CardHand.OnAddCard += OnAddCard;
			CardHand.OnRemoveCard += OnRemoveCard;
		}

		protected virtual void OnDestroy()
		{
			Input.OnPointerUp -= OnPointerUp;

			CardHand.OnAddCard -= OnAddCard;
			CardHand.OnRemoveCard -= OnRemoveCard;
		}

        protected virtual void OnPointerUp(PointerEventData eventData)
        {
			
        }


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

		void OnAddCard(IUiCard card) 
		{
			card.Input.OnPointerUp += OnPointerUpInternal;
		}

		void OnRemoveCard(IUiCard card) 
		{
			card.Input.OnPointerUp -= OnPointerUpInternal;
		}

		
	}
}