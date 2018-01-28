using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultShit : MonoBehaviour {

	Button returnHomeButton;

	void Awake()
	{
		returnHomeButton = GameObject.Find("Button").GetComponent<Button>();
		returnHomeButton.onClick.AddListener(ReturnHome);
	}
	
	void ReturnHome()
	{
		GameMaster.gameMaster.messageCounter = 0;
		GameMaster.gameMaster.LoadScene("StephensScene");
		
	}

}
