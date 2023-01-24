using System;
using System.Collections;
using UnityEngine;

namespace Tools.UI.Card
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Card graveyard holds a register with cards played by the player.
    /// </summary>
    public class UiCardGraveyard : UiCardPile
    {
        [SerializeField] [Tooltip("World point where the graveyard is positioned")]
        Transform graveyardPosition;

        //--------------------------------------------------------------------------------------------------------------

        IUiPlayerHand PlayerHand { get; set; }



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
		}

        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        protected override void Awake()
        {
            base.Awake();
            PlayerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
            PlayerHand.OnCardPlayed += OnCardPlayed;

			//PlayerHand.OnCardPlayed += AddCard;
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
        }


        /// <summary>
        ///     Removes a card from the graveyard or discard pile.
        /// </summary>
        /// <param name="card"></param>
        public override void RemoveCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);
            NotifyPileChange();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}