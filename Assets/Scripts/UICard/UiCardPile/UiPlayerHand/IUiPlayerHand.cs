using System;
using System.Collections.Generic;
using UnityEngine;

namespace UICard
{
	public interface IUiPlayerHand : IUiCardPile
	{
		Transform PlayTransform { get; }

		List<IUiCard> Cards { get; }
		
		Action<IUiCard> onCardPlayed { get; set; }
		Action<IUiCard> onCardSelected { get; set; }
		
		void PlaySelected();
		void Unselect();
		void PlayCard(IUiCard uiCard);
		void SelectCard(IUiCard uiCard);
		void UnselectCard(IUiCard uiCard);
	}
}