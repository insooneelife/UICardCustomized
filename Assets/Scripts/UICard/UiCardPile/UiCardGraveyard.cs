using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace UICard
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Card graveyard holds a register with cards played by the player.
    /// </summary>
    public class UiCardGraveyard : UiCardPile
    {
        [SerializeField] [Tooltip("World point where the graveyard is positioned")]
        Transform graveyardPosition;

		[SerializeField]
		Pool.SetPooler cardPool;

		[SerializeField] UiCardParameters parameters;

		//--------------------------------------------------------------------------------------------------------------

		IUiPlayerHand PlayerHand { get; set; }


		private void OnCardArrived(IUiCard card)
		{
			RemoveCard(card);
		}

		private void OnCardPlayed(IUiCard card)
		{
			card.Play();
			
			
			StartCoroutine(CoPlayCard(card));
		}


		IEnumerator CoPlayCard(IUiCard card)
		{
			while (card.Movement.IsOperating)
			{
				yield return null;
			}

			AddCard(card);

			Sort(Cards);
		}

        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        protected override void Awake()
        {
            base.Awake();
            PlayerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
            PlayerHand.OnCardPlayed += OnCardPlayed;
		}

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Adds a card to the graveyard or discard pile.
        /// </summary>
        /// <param name="card"></param>
        public override void AddCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(graveyardPosition);
            card.Discard();
            NotifyPileChange();

			card.Movement.onFinishMotion += OnCardArrived;

		}


        /// <summary>
        ///     Removes a card from the graveyard or discard pile.
        /// </summary>
        /// <param name="card"></param>
        public override void RemoveCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

			card.Movement.onFinishMotion -= OnCardArrived;

			card.Clear();

			Cards.Remove(card);

			cardPool.EnqueueObject(card.gameObject);


			NotifyPileChange();
        }

		#endregion


		private void Sort( List<IUiCard> cards)
		{
			var lastPos = cards.Count - 1;
			var lastCard = cards[lastPos];
			var gravPos = graveyardPosition.position + new Vector3(0, 0, -5);
			var backGravPos = graveyardPosition.position;

			//move last
			lastCard.MoveToWithZ(gravPos, parameters.MovementSpeed);

			//move others
			for (var i = 0; i < cards.Count - 1; i++)
			{
				var card = cards[i];
				card.MoveToWithZ(backGravPos, parameters.MovementSpeed);
			}

		}

		//--------------------------------------------------------------------------------------------------------------
	}
}