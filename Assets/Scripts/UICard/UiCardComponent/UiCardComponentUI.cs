using System.Collections;
using System.Collections.Generic;
using UICard;
using UnityEngine;
using UnityEngine.UI;

public class UiCardComponentUI : UiCardComponentBase
{
	const int LayerToRenderNormal = 0;
	const int LayerToRenderTop = 1;

	[SerializeField]
	private GameObject _uiPrefab;

	[SerializeField]
	private Canvas _canvas;

	private Graphic[] _graphics;
	
	protected override void Awake()
	{
		base.Awake();

		GameObject.Instantiate(_uiPrefab, _canvas.transform);
		_graphics = GetComponentsInChildren<Graphic>();	
	}


	// Renders the textures in the first layer. Each card state is responsible to handle its own layer activity.
	public override void MakeRenderFirst()
	{
		_canvas.sortingOrder = LayerToRenderTop;
	}



	// Renders the textures in the regular layer. Each card state is responsible to handle its own layer activity.
	public override void MakeRenderNormal()
	{
		_canvas.sortingOrder = LayerToRenderNormal;
	}


	public override void ApplyAllTransparency()
	{
		foreach (var _graphic in _graphics)
		{
			var myColor = _graphic.color;
			myColor.a = _cardConfigsParameters.DisabledAlpha;
			_graphic.color = myColor;
		}
	}

	// Remove any alpha channel in all renderers.
	public override void RemoveAllTransparency()
	{
		foreach (var _graphic in _graphics)
		{
			if (_graphic != null)
			{
				var myColor = _graphic.color;
				myColor.a = 1;
				_graphic.color = myColor;
			}
		}
	}
}
