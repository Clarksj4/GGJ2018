using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultShit : MonoBehaviour {

	Button returnHomeButton;
	public AudioClip Ending1;
	public AudioClip Ending2;
	public AudioClip Ending3;
	public AudioClip Ending4;
	public AudioClip Ending5;

	void Awake()
	{
		returnHomeButton = GameObject.Find("Button").GetComponent<Button>();
		returnHomeButton.onClick.AddListener(ReturnHome);
	}

	void Start()
	{
		CalculatePlayerTitle();
	}
	
	void ReturnHome()
	{
		GameMaster.gameMaster.messageCounter = 0;
		GameMaster.gameMaster.LoadScene("StephensScene");
		
	}

	void CalculatePlayerTitle()
	{
		string title = "";
		GameMaster gm = GameMaster.gameMaster;
		if (gm.CIAToCIACount == gm.CIAMax)
		{
			title = gm.Results[2];
			//Ending3.Play();
		}
		else if (gm.CIAToGrandmaCount > 2)
		{
			title = gm.Results[0];
			//Ending1.Play();
		}
		else if (gm.CIAToIRCount > 2)
		{
			title = gm.Results[3];
			//Ending4.Play();
		}
		else if (gm.CIAToCIACount > 2)
		{
			title = gm.Results[1];
			//Ending2.Play();
		}
		else
		{
			title = gm.Results[4];
			//Ending5.Play();
		}
	}

}
