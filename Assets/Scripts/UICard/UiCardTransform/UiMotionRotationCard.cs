using UnityEngine;

namespace UICard
{
	public class UiMotionRotationCard : UiMotionBaseCard
	{

		public UiMotionRotationCard(IUiCard handler)
			:
			base(handler)
		{ }

		protected override float Threshold
		{
			get { return 0.05f; }
		}


		protected override void OnMotionEnds()
		{
			_handler.transform.eulerAngles = _target;
			_isOperating = false;

			onFinishMotion?.Invoke(_handler);
		}

		protected override void KeepMotion()
		{
			Quaternion current = _handler.transform.rotation;
			float amount = _speed * Time.deltaTime;
			Quaternion rotation = Quaternion.Euler(_target);
			Quaternion newRotation = Quaternion.RotateTowards(current, rotation, amount);
			_handler.transform.rotation = newRotation;
		}

		protected override bool CheckFinalState()
		{
			Vector3 distance = _target - _handler.transform.eulerAngles;
			bool smallerThanLimit = distance.magnitude <= Threshold;
			bool equals360 = (int)distance.magnitude == 360;
			bool isFinal = smallerThanLimit || equals360;
			return isFinal;
		}
		
	}
}