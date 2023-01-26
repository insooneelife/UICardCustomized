using UnityEngine;

namespace UICard
{
	public class UiMotionMovementCard : UiMotionBaseCard
	{
		private bool _withZ;

		public UiMotionMovementCard(IUiCard handler) 
			:
			base(handler)
		{}

		public override void Execute(Vector3 position, float speed, float delay, bool withZ)
		{
			_withZ = withZ;
			base.Execute(position, speed, delay, withZ);
		}

		protected override void OnMotionEnds()
		{
			_withZ = false;
			_isOperating = false;
			Vector3 target = _target;
			target.z = _handler.transform.position.z;
			_handler.transform.position = target;
			base.OnMotionEnds();
		}

		protected override void KeepMotion()
		{
			Vector3 current = _handler.transform.position;
			float amount = _speed * Time.deltaTime;
			Vector3 delta = Vector3.Lerp(current, Target, amount);
			if (!_withZ)
			{ 
				delta.z = _handler.transform.position.z;
			}
			_handler.transform.position = delta;
		}

		protected override bool CheckFinalState()
		{
			Vector3 distance = _target - _handler.transform.position;
			if (!_withZ)
			{ 
				distance.z = 0;
			}
			return distance.magnitude <= Threshold;
		}
	}
}