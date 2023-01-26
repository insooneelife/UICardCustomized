using UnityEngine;

namespace UICard
{
	// Main components of an UI card.
	public interface IUiCardComponents
	{
		Camera MainCamera { get; }

		Bounds Bounds { get; }

		Collider Collider { get; }
		Rigidbody Rigidbody { get; }
		IMouseInput Input { get; }
		MonoBehaviour MonoBehavior { get; }
		GameObject gameObject { get; }
		Transform transform { get; }
		
		void MakeRenderFirst();

		void MakeRenderNormal();

		void ApplyAllTransparency();

		void RemoveAllTransparency();

		void DisableCollision();
		
		void EnableCollision();

	}
}