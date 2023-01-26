using System;
using Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UICard
{
	public class BendData
	{
		public Vector3 rotateEuler;
		public Vector3 movePosition;
	}

	// Class responsible to bend the cards in the player hand.
	[RequireComponent(typeof(IUiPlayerHand))]
	public class UiPlayerHandBender : MonoBehaviour
	{
		[SerializeField]
		private UiCardParameters _parameters;

		[SerializeField]
		[Tooltip("The Card Prefab")]
		private GameObject _cardPrefab;

		[SerializeField]
		[Tooltip("Transform used as anchor to position the cards.")]
		private Transform _pivot;

		private Bounds _cardBounds;
		private IUiPlayerHand _playerHand;

		private float CardWidth 
		{
			get { return _cardBounds.size.x; }
		}
		
		private void Awake()
		{
			_playerHand = GetComponent<IUiPlayerHand>();
			_cardBounds = _cardPrefab.GetComponentInChildren<IUiCard>().Bounds;			
			_playerHand.onPileChanged += Bend;
		}

		
		private void OnDestroy()
		{
			_playerHand.onPileChanged -= Bend;
		}
		

		private void OnEnterHover(IUiCard card)
		{
			Bend(_playerHand.Cards.ToArray());
		}

		private void OnExitHover(IUiCard card)
		{
			Bend(_playerHand.Cards.ToArray());
		}
				

		#region Operations

		private void Bend(IUiCard[] cards)
		{
			if (cards == null)
			{ 
				throw new ArgumentException("Can't bend a card list null");
			}

			float fullAngle = -_parameters.BentAngle;
			float anglePerCard = fullAngle / cards.Length;
			float firstAngle = CalcFirstAngle(fullAngle);
			float handWidth = CalcHandWidth(cards.Length);

			int pivotLocationFactor = _pivot.CloserEdge(Camera.main, Screen.width, Screen.height);

			//calc first position of the offset on X axis
			float offsetX = _pivot.position.x - handWidth / 2;

			for (int i = 0; i < cards.Length; i++)
			{
				IUiCard card = cards[i];

				//set card Z angle
				float angleTwist = (firstAngle + i * anglePerCard) * pivotLocationFactor;

				//calc x position
				float xPos = offsetX + CardWidth / 2;

				//calc y position
				float yDistance = Mathf.Abs(angleTwist) * _parameters.Height;
				float yPos = _pivot.position.y - yDistance * pivotLocationFactor;

				int zAxisRot = pivotLocationFactor == 1 ? 0 : 180;
				Vector3 rotation = new Vector3(0, 0, angleTwist - zAxisRot);
				Vector3 position = new Vector3(xPos, yPos, card.transform.position.z);
				float rotSpeed = card.IsPlayer ? _parameters.RotationSpeed : _parameters.RotationSpeedP2;

				//set position
				if (!card.IsDragging && !card.IsHovering)
				{
					card.RotateTo(rotation, rotSpeed);
					card.MoveTo(position, _parameters.MovementSpeed);
					card.CacheBendData = null;
				}
				else
				{
					BendData bendData = new BendData();
					bendData.rotateEuler = rotation;
					bendData.movePosition = position;
					card.CacheBendData = bendData;
				}

				//increment offset
				offsetX += CardWidth + _parameters.Spacing;
			}
		}
		

		// Calculus of the width of the player's hand.
		private float CalcHandWidth(int quantityOfCards)
		{
			float widthCards = quantityOfCards * CardWidth;
			float widthSpacing = (quantityOfCards - 1) * _parameters.Spacing;
			return widthCards + widthSpacing;
		}


		// Calculus of the angle of the first card.
		private static float CalcFirstAngle(float fullAngle)
		{
			float magicMathFactor = 0.1f;
			return -(fullAngle / 2) + fullAngle * magicMathFactor;
		}

		#endregion

	}
}