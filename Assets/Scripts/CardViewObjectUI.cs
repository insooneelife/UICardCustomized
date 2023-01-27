using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CardViewObjectUI
	:
	MonoBehaviour
{
	[SerializeField]
	protected Transform _topRight;

	[SerializeField]
	protected Image _cardFrontImg;

	[SerializeField]
	protected Image _cardBackImg;

	[SerializeField]
	protected GameObject _thumbnailObject;

	[SerializeField]
	protected Image _thumbnailImg;

	[SerializeField]
	protected GameObject _typeObject;

	[SerializeField]
	protected GameObject _nameObject;

	[SerializeField]
	protected GameObject _costObject;

	[SerializeField]
	protected GameObject _descObject;

	[SerializeField]
	protected TMP_Text _nameText;

	[SerializeField]
	protected TMP_Text _costText;

	[SerializeField]
	protected TMP_Text _descText;

	[SerializeField]
	protected GameObject _cardHighlightObject;
	
	[SerializeField]
	private GameObject _eventSucker;
	
	protected float _baseWidth;
	protected float _baseHeight;

	public Transform TopRight
	{
		get { return _topRight; }
	}

	public Image CardFrontImg
	{
		get { return _cardFrontImg; }
	}

	public Image CardBackImg
	{
		get { return _cardBackImg; }
	}

	public GameObject ThumbnailObject
	{
		get { return _thumbnailObject; }
	}

	public Image ThumbnailImg
	{
		get { return _thumbnailImg; }
	}

	public GameObject TypeObject
	{
		get { return _typeObject; }
	}

	public GameObject NameObject
	{
		get { return _nameObject; }
	}

	public GameObject CostObject
	{
		get { return _costObject; }
	}

	public GameObject DescObject
	{
		get { return _descObject; }
	}


	public TMP_Text NameText
	{
		get { return _nameText; }
	}

	public TMP_Text CostText
	{
		get { return _costText; }
	}

	public TMP_Text DescText
	{
		get { return _descText; }
	}

	public GameObject CardHighlightObject
	{
		get { return _cardHighlightObject; }
	}
	
	public GameObject EventSucker
	{
		get { return _eventSucker; }
	}

	public float BaseWidth
	{
		get { return _baseWidth; }
	}

	public float BaseHeight
	{
		get { return _baseHeight; }
	}

	public Vector3 TopLeftWorldPos
	{
		get
		{
			Vector3 basePos = transform.position;
			Vector3 topRightPos = _topRight.transform.position;

			Vector3 distanceXY = (topRightPos - basePos);
			Vector3 topLeft = new Vector3(basePos.x - distanceXY.x, topRightPos.y);

			return topLeft;
		}
	}

	public Vector3 BottomRightWorldPos
	{
		get
		{
			Vector3 basePos = transform.position;
			Vector3 topRightPos = _topRight.transform.position;

			Vector3 distanceXY = (topRightPos - basePos);
			Vector3 bottomRight = new Vector3(basePos.x + distanceXY.x, basePos.y - distanceXY.y);

			return bottomRight;
		}
	}

	public Vector3 BottomLeftWorldPos
	{
		get
		{
			Vector3 basePos = transform.position;
			Vector3 topRightPos = _topRight.transform.position;

			Vector3 distanceXY = (topRightPos - basePos);
			Vector3 bottomLeft = new Vector3(basePos.x - distanceXY.x, basePos.y - distanceXY.y);

			return bottomLeft;
		}
	}



	private void Awake()
	{
		RectTransform rectTrans = GetComponent<RectTransform>();
		_baseWidth = rectTrans.rect.width;
		_baseHeight = rectTrans.rect.height;
	}

}