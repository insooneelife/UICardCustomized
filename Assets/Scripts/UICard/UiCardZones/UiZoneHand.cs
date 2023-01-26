using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine;

namespace UICard
{
	// GameController hand zone.
	public class UiZoneHand : UiBaseDropZone
	{
		protected override void OnPointerUp(PointerEventData eventData)
		{
			_cardHand?.Unselect();
		}
	}
}