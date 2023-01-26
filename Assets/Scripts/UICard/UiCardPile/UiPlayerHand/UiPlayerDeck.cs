using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UICard
{
	public class UiPlayerDeck : MonoBehaviour
	{		
		[SerializeField]
		private GameObject _cardPrefab;

		[SerializeField]
		[Tooltip("World point where the deck is positioned")]
		private Transform _deckPosition;

		[SerializeField]
		[Tooltip("Game view transform")]
		private Transform _gameView;

		[SerializeField]
		private Pool.SetPooler _cardPool;

		private int _count;

		private IUiPlayerHand _playerHand;
		
		
		private void Awake()
		{
			_playerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
		}
				
		
		public void DrawCard()
		{
			Pool.Poolable cardPoolable = _cardPool.Dequeue();

			cardPoolable.name = "Card_" + _count;
			cardPoolable.transform.SetParent(_gameView);


			cardPoolable.gameObject.SetActive(true);

			IUiCard card = cardPoolable.GetComponent<IUiCard>();
			card.Init();

			cardPoolable.transform.rotation = _cardPrefab.transform.rotation;
			cardPoolable.transform.localScale = _cardPrefab.transform.localScale;
			card.transform.position = _deckPosition.position;
			_count++;
			_playerHand.AddCard(card);
		}

		public void PlayCard()
		{
			if (_playerHand.Cards.Count > 0)
			{
				IUiCard randomCard = _playerHand.Cards.Random();
				_playerHand.PlayCard(randomCard);
			}
		}		
		
	}
}