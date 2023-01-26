using System;
using System.Collections;
using UnityEngine;

namespace UICard
{
	public abstract class UiMotionBaseCard
	{
		// Dispatches when the motion ends.
		public Action<IUiCard> onFinishMotion { get; set; }

		protected bool _isOperating;
		
		protected Vector3 _target;

		protected IUiCard _handler;

		protected float _speed;


		// Whether the component is still operating or not.
		public bool IsOperating 
		{
			get { return _isOperating; } 
		}

		// Limit magnitude until the reaches the target completely.
		protected virtual float Threshold
		{
			get { return 0.05f; }
		}

		// Target of the motion.
		public Vector3 Target 
		{
			get { return _target; }
		}

		// Reference for the card.
		protected IUiCard Handler 
		{
			get { return _handler; } 
		}
		

		protected UiMotionBaseCard(IUiCard handler)
		{
			_handler = handler;
		}

		
		public void Update()
		{
			if (!IsOperating)
			{ 
				return;
			}

			if (CheckFinalState())
			{
				OnMotionEnds();
			}
			else
			{ 
				KeepMotion();
			}
		}

		public void Clear()
		{
			onFinishMotion = null;
		}

		// Check if it has reached the threshold.
		protected abstract bool CheckFinalState();

		// Ends the motion and dispatch motion ends.
		protected virtual void OnMotionEnds()
		{
			onFinishMotion?.Invoke(Handler);
		}


		// Keep the motion on update.
		protected abstract void KeepMotion();

		// Execute the motion with the parameters.
		public virtual void Execute(Vector3 vector, float speed, float delay = 0, bool withZ = false)
		{
			_speed = speed;
			_target = vector;
			if (delay == 0)
			{
				_isOperating = true;
			}
			else
			{ 
				_handler.MonoBehavior.StartCoroutine(AllowMotion(delay));
			}
		}

		// Used to delay the Motion.
		private IEnumerator AllowMotion(float delay)
		{
			yield return new WaitForSeconds(delay);
			_isOperating = true;
		}

		// Stop the motion. It won't trigger OnFinishMotion.
		// TODO: Cancel the Delay Coroutine.
		public virtual void StopMotion() 
		{
			_isOperating = false;
		} 
	}
}