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
	private AudioSource audioSource;
	private Text resultValue;

	private float audioMultiplier = 8f;

	void Awake()
	{
		returnHomeButton = GameObject.Find("Button").GetComponent<Button>();
		returnHomeButton.onClick.AddListener(ReturnHome);
		audioSource = GetComponent<AudioSource>();
		resultValue = GameObject.Find("ResultValue").GetComponent<Text>();
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
			audioSource.PlayOneShot(Ending3, audioMultiplier);
		}
		else if (gm.CIAToGrandmaCount > 2)
		{
			title = gm.Results[0];
			audioSource.PlayOneShot(Ending1, audioMultiplier);
		}
		else if (gm.CIAToIRCount > 2)
		{
			title = gm.Results[3];
			audioSource.PlayOneShot(Ending4, audioMultiplier);
		}
		else if (gm.CIAToCIACount > 2)
		{
			title = gm.Results[1];
			audioSource.PlayOneShot(Ending2,audioMultiplier);
		}
		else
		{
			title = gm.Results[4];
			audioSource.PlayOneShot(Ending5, audioMultiplier);
		}
		resultValue.text = title;

		Text grandmaPoints = GameObject.Find("GrandmaPointsValue").GetComponent<Text>();
		grandmaPoints.text = gm.GrandmaCount + " / " + gm.GrandmaMax;
	}

}
