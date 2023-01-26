using Patterns.StateMachine;

namespace UICard
{

	// This state disables the collider of the card.
	public class UiCardDisable : UiBaseCardState
	{
		public UiCardDisable(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) 
			: base(handler, fsm, parameters)
		{}

		public override void OnEnterState() 
		{
			Disable();
		}
	}
}