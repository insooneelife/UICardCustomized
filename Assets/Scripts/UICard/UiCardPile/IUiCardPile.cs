using System;

namespace UICard
{
	// A pile of cards.
	public interface IUiCardPile
	{
		Action<IUiCard[]> onPileChanged { get; set; }

		Action<IUiCard> onAddCard { get; set; }

		Action<IUiCard> onRemoveCard { get; set; }


		void AddCard(IUiCard uiCard);
		void RemoveCard(IUiCard uiCard);
	}
}