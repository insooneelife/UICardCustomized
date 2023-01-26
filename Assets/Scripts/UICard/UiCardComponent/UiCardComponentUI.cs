using System.Collections;
using System.Collections.Generic;
using UICard;
using UnityEngine;
using UnityEngine.UI;

public class UiCardComponentUI : UiCardComponentBase
{
	[SerializeField]
	private Image _image;

	const int LayerToRenderNormal = 0;
	const int LayerToRenderTop = 1;


	private SpriteRenderer[] _renderers;
	private SpriteRenderer _renderer;


	protected override void Awake()
	{
		base.Awake();

		_renderers = GetComponentsInChildren<SpriteRenderer>();
		_renderer = GetComponent<SpriteRenderer>();
	}


	// Renders the textures in the first layer. Each card state is responsible to handle its own layer activity.
	public override void MakeRenderFirst()
	{
		for (int i = 0; i < _renderers.Length; i++)
		{
			_renderers[i].sortingOrder = LayerToRenderTop;
		}
	}



	// Renders the textures in the regular layer. Each card state is responsible to handle its own layer activity.
	public override void MakeRenderNormal()
	{
		for (int i = 0; i < _renderers.Length; i++)
		{
			if (_renderers[i])
			{
				_renderers[i].sortingOrder = LayerToRenderNormal;
			}
		}
	}


	public override void ApplyAllTransparency()
	{
		foreach (var renderer in _renderers)
		{
			var myColor = renderer.color;
			myColor.a = _cardConfigsParameters.DisabledAlpha;
			renderer.color = myColor;
		}
	}

	// Remove any alpha channel in all renderers.
	public override void RemoveAllTransparency()
	{
		foreach (var renderer in _renderers)
		{
			if (renderer != null)
			{
				var myColor = renderer.color;
				myColor.a = 1;
				renderer.color = myColor;
			}
		}
	}
}
