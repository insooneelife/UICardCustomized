using Patterns.StateMachine;

namespace UICard
{

	// A complete UI card.
	public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
	{
		IUiPlayerHand Hand { get; }

		BendData CacheBendData { get; set; }

		bool IsDragging { get; }
		bool IsHovering { get; }
		bool IsDisabled { get; }
		bool IsPlayer { get; }

		EnumTypes.CardHowToUses CardHowToUse { get; }

		void Init();
		void Clear();

		void Disable();
		void Enable();
		void Select();
		void Unselect();
		void Hover();
		void Draw();
		void Discard();
		void Play();

	}
}