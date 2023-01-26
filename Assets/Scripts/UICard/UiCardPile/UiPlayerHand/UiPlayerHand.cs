using System;
using System.Diagnostics;
using UnityEngine;

namespace UICard
{
	// The Player Hand.
	public class UiPlayerHand : UiCardPile, IUiPlayerHand
	{
		[SerializeField]
		private Transform _playTransform;

		private IUiCard _selectedCard;


		public Transform PlayTransform
		{
			get { return _playTransform; }
		}
		

		public Action<IUiCard> onCardPlayed { get; set; }
		public Action<IUiCard> onCardSelected { get; set; }
		
		
		#region Operations

		// Select the card in the parameter.
		public void SelectCard(IUiCard card)
		{
			_selectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

			//disable all cards
			DisableCards();
			NotifyCardSelected();
		}

		// Play the card which is currently selected. Nothing happens if current is null.
		public void PlaySelected()
		{
			if (_selectedCard == null)
				return;

			PlayCard(_selectedCard);
		}

		// Play the card in the parameter.
		public void PlayCard(IUiCard card)
		{
			if (card == null)
			{ 
				throw new ArgumentNullException("Null is not a valid argument.");
			}

			_selectedCard = null;
			RemoveCard(card);
			onCardPlayed?.Invoke(card);
			EnableCards();
			NotifyPileChange();
		}

		// Unselect the card in the parameter.
		public void UnselectCard(IUiCard card)
		{
			if (card == null)
				return;

			_selectedCard = null;
			card.Unselect();
			NotifyPileChange();
			EnableCards();
		}

		// Unselect the card which is currently selected. Nothing happens if current is null.
		public void Unselect()
		{ 
			UnselectCard(_selectedCard);
		}
			

		// Disables input for all cards.
		public void DisableCards()
		{
			foreach (var otherCard in Cards)
			{ 
				otherCard.Disable();
			}
		}

		// Enables input for all cards.
		public void EnableCards()
		{
			foreach (var otherCard in Cards)
			{ 
				otherCard.Enable();
			}
		}

		private void NotifyCardSelected()
		{
			onCardSelected?.Invoke(_selectedCard);
		}		


		#endregion
	}
}