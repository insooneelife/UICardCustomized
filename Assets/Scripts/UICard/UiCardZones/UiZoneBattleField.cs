using System.Diagnostics;
using UnityEngine.EventSystems;

namespace UICard
{
    /// <summary>
    ///     Battlefield Zone.
    /// </summary>
    public class UiZoneBattleField : UiBaseDropZone
    {
		protected override void OnPointerUp(PointerEventData eventData)
		{
			CardHand?.PlaySelected();
		}

    }
}