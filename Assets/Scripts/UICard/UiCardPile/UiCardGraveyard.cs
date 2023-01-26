using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace UICard
{

	// Card graveyard holds a register with cards played by the player.
	public class UiCardGraveyard : UiCardPile
	{
		[SerializeField]
		[Tooltip("World point where the graveyard is positioned")]
		private Transform _graveyardPosition;

		[SerializeField]
		private Pool.SetPooler _cardPool;

		[SerializeField]
		private UiCardParameters _parameters;

		private IUiPlayerHand _playerHand;
		
		protected override void Awake()
		{
			base.Awake();
			_playerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
			_playerHand.onCardPlayed += OnCardPlayed;
		}

		private void OnDestroy()
		{
			_playerHand.onCardPlayed -= OnCardPlayed;
		}


		// Adds a card to the graveyard or discard pile.
		public override void AddCard(IUiCard card)
		{
			if (card == null)
			{
				throw new ArgumentNullException("Null is not a valid argument.");
			}

			_cards.Add(card);
			card.transform.SetParent(_graveyardPosition);
			card.Discard();
			NotifyPileChange();
			card.Movement.onFinishMotion += OnCardArrived;
		}

		// Removes a card from the graveyard or discard pile.
		public override void RemoveCard(IUiCard card)
		{
			if (card == null)
			{
				throw new ArgumentNullException("Null is not a valid argument.");
			}

			card.Movement.onFinishMotion -= OnCardArrived;
			card.Clear();
			_cards.Remove(card);
			_cardPool.EnqueueObject(card.gameObject);
			NotifyPileChange();
		}

		private void OnCardPlayed(IUiCard card)
		{
			card.Play();
			StartCoroutine(CoPlayCard(card));
		}


		private IEnumerator CoPlayCard(IUiCard card)
		{
			while (card.Movement.IsOperating)
			{
				yield return null;
			}

			AddCard(card);
			Sort(_cards);
		}

		private void OnCardArrived(IUiCard card)
		{
			RemoveCard(card);
		}
		


		private void Sort(List<IUiCard> cards)
		{
			int lastPos = cards.Count - 1;
			IUiCard lastCard = cards[lastPos];
			Vector3 gravPos = _graveyardPosition.position + new Vector3(0, 0, -5);
			Vector3 backGravPos = _graveyardPosition.position;

			//move last
			lastCard.MoveToWithZ(gravPos, _parameters.MovementSpeed);

			//move others
			for (int i = 0; i < cards.Count - 1; i++)
			{
				IUiCard card = cards[i];
				card.MoveToWithZ(backGravPos, _parameters.MovementSpeed);
			}

		}

	}
}