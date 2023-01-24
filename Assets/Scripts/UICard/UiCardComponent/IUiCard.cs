﻿using Patterns.StateMachine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     A complete UI card.
    /// </summary>
    public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
    {
        IUiPlayerHand Hand { get; }

		BendData CacheBendData { get; set; }

		bool IsDragging { get; }
        bool IsHovering { get; }
        bool IsDisabled { get; }
        bool IsPlayer { get; }

		bool UseTargetGround { get; set; }
		
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