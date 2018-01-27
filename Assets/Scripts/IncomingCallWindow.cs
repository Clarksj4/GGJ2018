using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomingCallWindow : MonoBehaviour {

	private Button pickUpButton;
	private bool callIsUnderway;
	private AudioSource audioSource;

	void Awake()
	{
		pickUpButton = GetComponentInChildren<Button>();
		pickUpButton.onClick.AddListener(Answer);
		audioSource = GetComponent<AudioSource>();

	}

	void Start ()
	{
	}

	public void Answer()
	{
		audioSource.Pause();
		pickUpButton.gameObject.SetActive(false);
	}
}
