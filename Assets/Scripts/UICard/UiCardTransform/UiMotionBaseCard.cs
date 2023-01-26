using System;
using System.Collections;
using UnityEngine;

namespace UICard
{
    public abstract class UiMotionBaseCard
    {
        /// <summary>
        ///     Dispatches when the motion ends.
        /// </summary>
        public Action<IUiCard> OnFinishMotion = (IUiCard card) => { };

        //--------------------------------------------------------------------------------------------------------------

        protected UiMotionBaseCard(IUiCard handler) => Handler = handler;

        /// <summary>
        ///     Whether the component is still operating or not.
        /// </summary>
        public bool IsOperating { get; protected set; }

        /// <summary>
        ///     Limit magnitude until the reaches the target completely.
        /// </summary>
        protected virtual float Threshold => 0.05f;

        /// <summary>
        ///     Target of the motion.
        /// </summary>
        public Vector3 Target { get; protected set; }

        /// <summary>
        ///     Reference for the card.
        /// </summary>
        protected IUiCard Handler { get; }

        /// <summary>
        ///     Speed which the it moves towards the Target.
        /// </summary>
        protected float Speed { get; set; }

        //--------------------------------------------------------------------------------------------------------------

        public void Update()
        {
            if (!IsOperating)
                return;

            if (CheckFinalState())
                OnMotionEnds();
            else
                KeepMotion();
        }

		public void Clear()
		{
			OnFinishMotion = null;
		}

        /// <summary>
        ///     Check if it has reached the threshold.
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckFinalState();

        /// <summary>
        ///     Ends the motion and dispatch motion ends.
        /// </summary>
        protected virtual void OnMotionEnds() => OnFinishMotion?.Invoke(Handler);

        /// <summary>
        ///     Keep the motion on update.
        /// </summary>
        protected abstract void KeepMotion();

        /// <summary>
        ///     Execute the motion with the parameters.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        public virtual void Execute(Vector3 vector, float speed, float delay = 0, bool withZ = false)
        {
            Speed = speed;
            Target = vector;
            if (delay == 0)
                IsOperating = true;
            else
                Handler.MonoBehavior.StartCoroutine(AllowMotion(delay));
        }

        /// <summary>
        ///     Used to delay the Motion.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        IEnumerator AllowMotion(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsOperating = true;
        }

        /// <summary>
        ///     Stop the motion. It won't trigger OnFinishMotion.
        ///     TODO: Cancel the Delay Coroutine.
        /// </summary>
        public virtual void StopMotion() => IsOperating = false;
    }
}