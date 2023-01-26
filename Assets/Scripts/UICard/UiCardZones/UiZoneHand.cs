using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine;

namespace UICard
{
    /// <summary>
    ///     GameController hand zone.
    /// </summary>
    public class UiZoneHand : UiBaseDropZone
    {
		protected override void OnPointerUp(PointerEventData eventData)
		{
			CardHand?.Unselect();
		}
	}
}