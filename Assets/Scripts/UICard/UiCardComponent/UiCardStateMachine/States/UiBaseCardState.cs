using Patterns.StateMachine;
using System.Diagnostics;

namespace UICard
{

	// Base UI Card State.
	public abstract class UiBaseCardState : IState
	{
		protected IUiCard _handler;
		protected UiCardParameters _parameters;
		protected BaseStateMachine _fsm;
		protected bool _isInitialized;

		protected IUiCard Handler
		{
			get { return _handler; }
		}

		protected UiCardParameters Parameters
		{
			get { return _parameters; }
		}

		protected BaseStateMachine Fsm
		{
			get { return _fsm; }
		}

		public bool IsInitialized
		{
			get { return _isInitialized; }
		}

		protected UiBaseCardState(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters)
		{
			_fsm = fsm;
			_handler = handler;
			_parameters = parameters;
			_isInitialized = true;
		}



		

		// Enables the card entirely. Collision, Rigidybody and adds Alpha.

		protected void Enable()
		{
			if (_handler.Collider != null)
			{
				_handler.EnableCollision();
			}

			if (_handler.Rigidbody != null)
			{ 
				_handler.Rigidbody.Sleep();
			}

			_handler.MakeRenderNormal();
			_handler.RemoveAllTransparency();
		}


		// Disables the card entirely. Collision, Rigidybody and adds Alpha.

		protected virtual void Disable()
		{
			_handler.DisableCollision();
			_handler.Rigidbody.Sleep();
			_handler.MakeRenderNormal();
			_handler.ApplyAllTransparency();
		}
		
		#region FSM

		public void OnInitialize()
		{
		}

		public virtual void OnEnterState()
		{
		}

		public virtual void OnExitState()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public virtual void OnNextState(IState next)
		{
		}

		public virtual void OnClear()
		{
		}

		#endregion

	}
}