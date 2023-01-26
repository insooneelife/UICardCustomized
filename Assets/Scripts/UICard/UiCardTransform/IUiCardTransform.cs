using UnityEngine;

namespace UICard
{
	// Interface for simple Transform operations.
	public interface IUiCardTransform
	{
		// Movement module.
		UiMotionBaseCard Movement { get; }

		// Rotation module.
		UiMotionBaseCard Rotation { get; }

		// Scale module.
		UiMotionBaseCard Scale { get; }

		// Move in the 3d space using only the X and Y axis.
		void MoveTo(Vector3 position, float speed, float delay = 0);

		// Move in the 3d space.
		void MoveToWithZ(Vector3 position, float speed, float delay = 0);

		// Rotate in the 3d space.
		void RotateTo(Vector3 euler, float speed);

		// Scale in the 3d space.
		void ScaleTo(Vector3 scale, float speed, float delay = 0);
	}
}