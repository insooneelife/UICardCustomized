using UnityEngine;
using UnityEngine.UI;

namespace UICard
{
	// Notifies the hand when the UI component is changed.
	public class UiHandNotify : MonoBehaviour
	{
		private void Awake()
		{
			UiPlayerHand[] cardHands = FindObjectsOfType<UiPlayerHand>();
			Button button = GetComponentInChildren<Button>();
			Slider slider = GetComponentInChildren<Slider>();

			foreach (var hand in cardHands)
			{
				if (button)
					button.onClick.AddListener(hand.NotifyPileChange);

				if (slider)
					slider.onValueChanged.AddListener(afloat => { hand.NotifyPileChange(); });
			}
		}
	}
}