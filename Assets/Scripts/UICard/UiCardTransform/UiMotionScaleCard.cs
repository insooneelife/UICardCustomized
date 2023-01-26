using UnityEngine;

namespace UICard
{
	public class UiMotionScaleCard : UiMotionBaseCard
	{
		public UiMotionScaleCard(IUiCard handler) 
			: base(handler) { }

		protected override bool CheckFinalState()
		{
			Vector3 delta = _target - _handler.transform.localScale;
			return delta.magnitude <= Threshold;
		}

		protected override void OnMotionEnds()
		{
			_handler.transform.localScale = _target;
			_isOperating = false;
		}

		protected override void KeepMotion()
		{
			Vector3 current = _handler.transform.localScale;
			float amount = Time.deltaTime * _speed;
			_handler.transform.localScale = Vector3.Lerp(current, _target, amount);
		}
	}
}