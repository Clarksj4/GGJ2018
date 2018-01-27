using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class GameMaster : MonoBehaviour {

    public static GameMaster gameMaster;
    public SceneManager sceneManager;

    public int CIAToGrandmaCount;
    public int CIAToCIACount;
    public int CIAToIRCount;
    public int CIAMax;


    public string[] Results = {
        "Fatally Inept",
        "Adequate",
        "Goodie Two-Shoes",
        "Sloppy",
        "Brainless"
    };

    
    
	void Awake()
    {
        if (gameMaster == null)
        {
            gameMaster = this;
        }
        else if (gameMaster != this)
        {
            Destroy(this.gameObject);
        }
        sceneManager = new SceneManager();
    }
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void HandleDrop (proper_destinations targetDestination, proper_destinations actualDestination)
    {
        Debug.Log("GameManager.HandleDrop()");

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
