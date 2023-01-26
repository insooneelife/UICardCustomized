using System;
using System.Collections;
using System.Collections.Generic;
using UICard;
using UnityEditor.PackageManager;
using UnityEngine;



public class TestGameManager : MonoBehaviour
{
	[SerializeField] private UiPlayerDeck hand;


	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			hand.DrawCard();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			hand.PlayCard();
		}
	}
}
