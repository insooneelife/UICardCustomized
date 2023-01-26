using UnityEngine;

namespace UICard
{
    /// <summary>
    ///     Enables or Disables a gameobject on Start.
    /// </summary>
    public class UiStartEnabler : MonoBehaviour
    {
		private bool _isActive;
		
        public bool IsActive
		{
			get { return _isActive; }
			set { _isActive = value; }
		}

		private void Start()
		{
			gameObject.SetActive(_isActive);
		} 
    }
}