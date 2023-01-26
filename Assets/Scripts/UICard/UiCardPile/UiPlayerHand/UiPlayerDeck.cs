using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UICard
{
    public class UiPlayerDeck : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Fields

        int Count { get; set; }

        [SerializeField] [Tooltip("Prefab of the Card C#")]
        GameObject cardPrefabCs;

        [SerializeField] [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;

        [SerializeField] [Tooltip("Game view transform")]
        Transform gameView;

		[SerializeField]
		Pool.SetPooler cardPool;

		IUiPlayerHand PlayerHand { get; set; }

		#endregion

		//--------------------------------------------------------------------------------------------------------------

		#region Unitycallbacks

		void Awake()
		{
			PlayerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
			
		}
		
		
		

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations
		
        public void DrawCard()
        {
			var cardPoolable = cardPool.Dequeue();
			
			cardPoolable.name = "Card_" + Count;
			cardPoolable.transform.SetParent(gameView);
		

			cardPoolable.gameObject.SetActive(true);

			var card = cardPoolable.GetComponent<IUiCard>();
			card.Init();

			cardPoolable.transform.rotation = cardPrefabCs.transform.rotation;
			cardPoolable.transform.localScale = cardPrefabCs.transform.localScale;
			card.transform.position = deckPosition.position;
            Count++;
            PlayerHand.AddCard(card);
        }
		
        public void PlayCard()
        {
            if (PlayerHand.Cards.Count > 0)
            {
                var randomCard = PlayerHand.Cards.Random();
                PlayerHand.PlayCard(randomCard);
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}