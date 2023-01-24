using System;

namespace Tools.UI.Card
{
    /// <summary>
    ///     A pile of cards.
    /// </summary>
    public interface IUiCardPile
    {
        Action<IUiCard[]> OnPileChanged { get; set; }

		Action<IUiCard> OnAddCard {	get; set; }

		Action<IUiCard> OnRemoveCard { get; set; }


		void AddCard(IUiCard uiCard);
        void RemoveCard(IUiCard uiCard);
    }
}