﻿using Extensions;
using UnityEngine;

namespace UICard
{
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(IMouseInput))]
	public class UiCardComponent : MonoBehaviour, IUiCard
	{
		[SerializeField]
		public UiCardParameters _cardConfigsParameters;

		private IUiPlayerHand _hand;
		private BendData _cacheBendData;

		private UiCardHandFsm _fsm;
		private Collider _collider;
		private SpriteRenderer[] _renderers;
		private SpriteRenderer _renderer;
		private Rigidbody _rigidbody;
		private IMouseInput _input;


		private EnumTypes.CardHowToUses _cardHowToUse;


		private UiMotionBaseCard _movement;
		private UiMotionBaseCard _rotation;
		private UiMotionBaseCard _scale;


		public string Name
		{
			get { return gameObject.name; }
		}

		public IUiPlayerHand Hand 
		{
			get { return _hand; } 
		} 

		public BendData CacheBendData 
		{
			get { return _cacheBendData; }
			set { _cacheBendData = value; }
		}

		public EnumTypes.CardHowToUses CardHowToUse 
		{
			get { return _cardHowToUse; } 
		}

		public bool IsDragging
		{
			get { return _fsm.IsCurrent<UiCardDrag>(); }
		}

		public bool IsHovering
		{
			get { return _fsm.IsCurrent<UiCardHover>(); }
		}

		public bool IsDisabled
		{
			get { return _fsm.IsCurrent<UiCardDisable>(); }
		}

		public bool IsPlayer
		{
			get { return transform.CloserEdge(MainCamera, Screen.width, Screen.height) == 1; }
		}
		

		public MonoBehaviour MonoBehavior 
		{
			get { return this; }
		} 
		
		public Camera MainCamera 
		{
			get { return Camera.main; }
		} 

		public SpriteRenderer[] Renderers 
		{
			get { return _renderers; }
		}

		public SpriteRenderer Renderer 
		{
			get { return _renderer; }
		}

		public Collider Collider
		{
			get { return _collider; }
		}

		public Rigidbody Rigidbody
		{
			get { return _rigidbody; }
		}
		
		public IMouseInput Input
		{
			get { return _input; }
		}


		public UiMotionBaseCard Movement 
		{
			get { return _movement; } 
		}
		
		public UiMotionBaseCard Rotation 
		{
			get { return _rotation; }
		}

		public UiMotionBaseCard Scale 
		{
			get { return _scale; }
		}


		

		private void Awake()
		{
			_collider = GetComponent<Collider>();
			_rigidbody = GetComponent<Rigidbody>();
			_input = GetComponent<IMouseInput>();

			_renderers = GetComponentsInChildren<SpriteRenderer>();
			_renderer = GetComponent<SpriteRenderer>();

			_scale = new UiMotionScaleCard(this);
			_movement = new UiMotionMovementCard(this);
			_rotation = new UiMotionRotationCard(this);

			_fsm = new UiCardHandFsm(MainCamera, _cardConfigsParameters, this);
		}
		
		private void Update()
		{
			_fsm?.Update();
			_movement?.Update();
			_rotation?.Update();
			_scale?.Update();
		}
		
		public void Init()
		{
			_hand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
			int dice = UnityEngine.Random.Range(0, 2);
			_cardHowToUse = dice == 0 ? EnumTypes.CardHowToUses.Normal : EnumTypes.CardHowToUses.TargetGround;
			GetComponent<UiTargetLineController>().Init(this);
		}

		public void Clear()
		{
			_scale.Clear();
			_movement.Clear();
			_rotation.Clear();
		}


		#region Transform

		public void RotateTo(Vector3 rotation, float speed) 
		{
			_rotation.Execute(rotation, speed);
		}

		public void MoveTo(Vector3 position, float speed, float delay)
		{
			_movement.Execute(position, speed, delay);
		}

		public void MoveToWithZ(Vector3 position, float speed, float delay)
		{
			_movement.Execute(position, speed, delay, true);
		}

		public void ScaleTo(Vector3 scale, float speed, float delay)
		{ 
			_scale.Execute(scale, speed, delay);
		}

		#endregion



		#region Forward Functions

		public void Hover()
		{ 
			_fsm.Hover();
		}

		public void Disable()
		{ 
			_fsm.Disable();
		}

		public void Enable()
		{ 
			_fsm.Enable();
		}

		public void Select()
		{
			// to avoid the player selecting enemy's cards
			if (!IsPlayer)
				return;

			_hand.SelectCard(this);
			_fsm.Select();
		}

		public void Unselect() 
		{ 
			_fsm.Unselect();
		} 

		public void Draw()
		{
			_fsm.Draw();
		}


		public void Discard()
		{
			_fsm.Discard();
		}

		public void Play()
		{
			_fsm.Play();
		}

		#endregion
		
	}
}