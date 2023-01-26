using Patterns.StateMachine;
using System.Diagnostics;

namespace UICard
{

	// Base UI Card State.
	public abstract class UiBaseCardState : IState
	{
		const int LayerToRenderNormal = 0;
		const int LayerToRenderTop = 1;

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



		// Renders the textures in the first layer. Each card state is responsible to handle its own layer activity.
		protected virtual void MakeRenderFirst()
		{
			for (int i = 0; i < _handler.Renderers.Length; i++)
			{ 
				_handler.Renderers[i].sortingOrder = LayerToRenderTop;
			}
		}



		// Renders the textures in the regular layer. Each card state is responsible to handle its own layer activity.
		protected virtual void MakeRenderNormal()
		{
			for (int i = 0; i < _handler.Renderers.Length; i++)
			{
				if (_handler.Renderers[i])
				{ 
					_handler.Renderers[i].sortingOrder = LayerToRenderNormal;
				}
			}
		}

		// Enables the card entirely. Collision, Rigidybody and adds Alpha.

		protected void Enable()
		{
			if (_handler.Collider)
			{ 
				EnableCollision();
			}

			if (_handler.Rigidbody)
			{ 
				_handler.Rigidbody.Sleep();
			}

			MakeRenderNormal();
			RemoveAllTransparency();
		}


		// Disables the card entirely. Collision, Rigidybody and adds Alpha.

		protected virtual void Disable()
		{
			DisableCollision();
			_handler.Rigidbody.Sleep();
			MakeRenderNormal();
			
			foreach (var renderer in _handler.Renderers)
			{
				var myColor = renderer.color;
				myColor.a = Parameters.DisabledAlpha;
				renderer.color = myColor;
			}
		}


		// Disables the collision with this card.

		protected virtual void DisableCollision()
		{
			_handler.Collider.enabled = false;
		}


		// Enables the collision with this card.

		protected virtual void EnableCollision()
		{
			_handler.Collider.enabled = true;
		}


		// Remove any alpha channel in all renderers.
		protected void RemoveAllTransparency()
		{
			foreach (var renderer in _handler.Renderers)
			{ 
				if (renderer != null)
				{
					var myColor = renderer.color;
					myColor.a = 1;
					renderer.color = myColor;
				}
			}
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