using System;
using System.Collections.Generic;
using UnityEngine;

namespace UICard
{
	// Pile of cards. Add or Remove cards and be notified when changes happen.
	public abstract class UiCardPile : MonoBehaviour, IUiCardPile
	{
		protected List<IUiCard> _cards;
		
		public List<IUiCard> Cards 
		{
			get { return _cards; } 
		}
		
		public Action<IUiCard[]> onPileChanged { get; set; }
		
		public Action<IUiCard> onAddCard { get; set; }

		public Action<IUiCard> onRemoveCard	{ get; set; }


		protected virtual void Awake()
		{
			//initialize register
			_cards = new List<IUiCard>();
			Clear();
		}


		#region Operations

		// Add a card to the pile.
		public virtual void AddCard(IUiCard card)
		{
			if (card == null)
			{ 
				throw new ArgumentNullException("Null is not a valid argument.");
			}

			_cards.Add(card);
			card.transform.SetParent(transform);
			NotifyPileChange();

			onAddCard?.Invoke(card);

			card.Draw();
		}

		
		// Remove a card from the pile.
		public virtual void RemoveCard(IUiCard card)
		{
			if (card == null)
			{ 
				throw new ArgumentNullException("Null is not a valid argument.");
			}

			_cards.Remove(card);

			NotifyPileChange();

			onRemoveCard?.Invoke(card);
		}

		// Clear all the pile.
		protected virtual void Clear()
		{
			IUiCard[] childCards = GetComponentsInChildren<IUiCard>();
			foreach (var uiCardHand in childCards)
			{ 
				Destroy(uiCardHand.gameObject);
			}

			_cards.Clear();
		}

		// Notify all listeners of this pile that some change has been made.
		public void NotifyPileChange()
		{
			if (_cards.Count > 0)
			{
				onPileChanged?.Invoke(_cards.ToArray());
			}
		}

		#endregion
		
	}
}