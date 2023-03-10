using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : BaseMonoBehaviour where T : Component
{

	#region Fields

	/// <summary>
	/// The instance.
	/// </summary>
	private static T instance;
	protected static bool applicationIsQuitting = false;
	#endregion

	#region Properties

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if (applicationIsQuitting)
			{
				Debug.LogError("Singleton Null");
				return null;
			}

			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

}