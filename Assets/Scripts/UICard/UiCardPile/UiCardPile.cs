using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UI.Card
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Pile of cards. Add or Remove cards and be notified when changes happen.
    /// </summary>
    public abstract class UiCardPile : MonoBehaviour, IUiCardPile
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        protected virtual void Awake()
        {
            //initialize register
            Cards = new List<IUiCard>();

            Clear();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Properties

        /// <summary>
        ///     List with all cards.
        /// </summary>
        public List<IUiCard> Cards { get; private set; }

        /// <summary>
        ///     Event raised when add or remove a card.
        /// </summary>
        event Action<IUiCard[]> onPileChanged = hand => { };

        public Action<IUiCard[]> OnPileChanged
        {
            get => onPileChanged;
            set => onPileChanged = value;
        }

		event Action<IUiCard> onAddCard = hand => { };

		public Action<IUiCard> OnAddCard
		{
			get => onAddCard;
			set => onAddCard = value;
		}

		event Action<IUiCard> onRemoveCard = hand => { };

		public Action<IUiCard> OnRemoveCard
		{
			get => onRemoveCard;
			set => onRemoveCard = value;
		}


		#endregion

		//--------------------------------------------------------------------------------------------------------------

		#region Operations

		/// <summary>
		///     Add a card to the pile.
		/// </summary>
		/// <param name="card"></param>
		public virtual void AddCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(transform);
            NotifyPileChange();

			onAddCard?.Invoke(card);

			card.Draw();
        }


        /// <summary>
        ///     Remove a card from the pile.
        /// </summary>
        /// <param name="card"></param>
        public virtual void RemoveCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);

            NotifyPileChange();

			onRemoveCard?.Invoke(card);
		}

        /// <summary>
        ///     Clear all the pile.
        /// </summary>
        protected virtual void Clear()
        {
            var childCards = GetComponentsInChildren<IUiCard>();
            foreach (var uiCardHand in childCards)
                Destroy(uiCardHand.gameObject);

            Cards.Clear();
        }

		/// <summary>
		///     Notify all listeners of this pile that some change has been made.
		/// </summary>
		public void NotifyPileChange()
		{
			if (Cards.Count > 0)
			{ 
				onPileChanged?.Invoke(Cards.ToArray());
			}
		} 

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}