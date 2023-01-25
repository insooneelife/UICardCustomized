using Extensions;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardComponent : MonoBehaviour, IUiCard
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Unity Callbacks

        public void Init()
        {
            //components
            MyTransform = transform;
            MyCollider = GetComponent<Collider>();
            MyRigidbody = GetComponent<Rigidbody>();
            MyInput = GetComponent<IMouseInput>();
            Hand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
            MyRenderers = GetComponentsInChildren<SpriteRenderer>();
            MyRenderer = GetComponent<SpriteRenderer>();

            //transform
            Scale = new UiMotionScaleCard(this);
            Movement = new UiMotionMovementCard(this);
            Rotation = new UiMotionRotationCard(this);

            //fsm
            Fsm = new UiCardHandFsm(MainCamera, cardConfigsParameters, this);

			int dice = UnityEngine.Random.Range(0, 2);
			
			CardHowToUse = dice == 0 ? EnumTypes.CardHowToUses.Normal : EnumTypes.CardHowToUses.TargetGround;

			GetComponent<UiTargetLineController>().Init(this);
        }

		public void Destroy()
		{
			
		}

        void Update()
        {
            Fsm?.Update();
            Movement?.Update();
            Rotation?.Update();
            Scale?.Update();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Components

        SpriteRenderer[] IUiCardComponents.Renderers => MyRenderers;
        SpriteRenderer IUiCardComponents.Renderer => MyRenderer;
        Collider IUiCardComponents.Collider => MyCollider;
        Rigidbody IUiCardComponents.Rigidbody => MyRigidbody;
        IMouseInput IUiCardComponents.Input => MyInput;
		
		IUiPlayerHand IUiCard.Hand => Hand;

		public BendData CacheBendData { get; set; }

		#endregion

		#region Transform

		public UiMotionBaseCard Movement { get; private set; }
        public UiMotionBaseCard Rotation { get; private set; }
        public UiMotionBaseCard Scale { get; private set; }

        #endregion

        #region Properties

        public string Name => gameObject.name;
        [SerializeField] 
		public UiCardParameters cardConfigsParameters;

		UiCardHandFsm Fsm { get; set; }
        Transform MyTransform { get; set; }
        Collider MyCollider { get; set; }
        SpriteRenderer[] MyRenderers { get; set; }
        SpriteRenderer MyRenderer { get; set; }
        Rigidbody MyRigidbody { get; set; }
        IMouseInput MyInput { get; set; }
        IUiPlayerHand Hand { get; set; }
        public MonoBehaviour MonoBehavior => this;
        public Camera MainCamera => Camera.main;
        public bool IsDragging => Fsm.IsCurrent<UiCardDrag>();
        public bool IsHovering => Fsm.IsCurrent<UiCardHover>();
        public bool IsDisabled => Fsm.IsCurrent<UiCardDisable>();
        public bool IsPlayer => transform.CloserEdge(MainCamera, Screen.width, Screen.height) == 1;

		public EnumTypes.CardHowToUses CardHowToUse { get; set; }

		#endregion

		//--------------------------------------------------------------------------------------------------------------

		#region Transform

		public void RotateTo(Vector3 rotation, float speed) => Rotation.Execute(rotation, speed);

        public void MoveTo(Vector3 position, float speed, float delay) => Movement.Execute(position, speed, delay);

        public void MoveToWithZ(Vector3 position, float speed, float delay) =>
            Movement.Execute(position, speed, delay, true);

        public void ScaleTo(Vector3 scale, float speed, float delay) => Scale.Execute(scale, speed, delay);

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public void Hover() => Fsm.Hover();

        public void Disable() => Fsm.Disable();

        public void Enable() => Fsm.Enable();

        public void Select()
        {
            // to avoid the player selecting enemy's cards
            if (!IsPlayer)
                return;

            Hand.SelectCard(this);
            Fsm.Select();
        }

        public void Unselect() => Fsm.Unselect();

		public void Draw()
		{ 
			Fsm.Draw();
		}


		public void Discard() 
		{ 
			Fsm.Discard();
		}

		public void Play() 
		{
			Fsm.Play();
		}

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}