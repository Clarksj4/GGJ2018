using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomingCallWindow : MonoBehaviour {

	private Button pickUpButton;
	private bool callIsUnderway;
	private AudioSource audioSource;
	public AudioClip tutorialClip;
	public AudioClip dialTone;

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
		audioSource.Stop();
		pickUpButton.gameObject.SetActive(false);
		GameObject.Find("IncomingCall").GetComponent<Text>().text = "ONGOING CALL";
		audioSource.PlayOneShot(tutorialClip, 10f);
		StartCoroutine(WaitForCallToEnd());
	}

	private IEnumerator WaitForCallToEnd ()
	{
		yield return new WaitForSeconds(55f);
		audioSource.PlayOneShot(dialTone);
		yield return new WaitForSeconds(2f);
		Destroy(this.gameObject);
	}
}
