using System.Diagnostics;
using UnityEngine.EventSystems;

namespace UICard
{
	// Battlefield Zone.
	public class UiZoneBattleField : UiBaseDropZone
	{
		protected override void OnPointerUp(PointerEventData eventData)
		{
			_cardHand?.PlaySelected();
		}

	}
}