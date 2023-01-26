using System;
using UnityEngine;

namespace UICard
{
	[RequireComponent(typeof(IUiPlayerHand))]
	public class UiPlayerHandSorter : MonoBehaviour
	{
		private const int OffsetZ = -1;
		private IUiCardPile _playerHand;
		

		private void Awake()
		{
			_playerHand = GetComponent<IUiPlayerHand>();
			_playerHand.onPileChanged += Sort;
		}

		private void OnDestroy()
		{
			_playerHand.onPileChanged += Sort;
		}
		
		private void Sort(IUiCard[] cards)
		{
			if (cards == null)
			{ 
				throw new ArgumentException("Can't sort a card list null");
			}

			int layerZ = 0;
			foreach (IUiCard card in cards)
			{
				Vector3 localCardPosition = card.transform.localPosition;
				localCardPosition.z = layerZ;
				card.transform.localPosition = localCardPosition;
				layerZ += OffsetZ;
			}
		}
		
	}
}