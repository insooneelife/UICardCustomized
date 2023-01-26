﻿using UnityEngine;

namespace UICard
{
    /// <summary>
    ///     Enables or Disables a gameobject on Start.
    /// </summary>
    public class UiStartEnabler : MonoBehaviour
    {
        public bool IsActive;

		private void Start()
		{
			gameObject.SetActive(IsActive);
		} 
    }
}