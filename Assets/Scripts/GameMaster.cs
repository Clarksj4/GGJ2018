using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static GameMaster gameMaster;
    public SceneManager sceneManager;
    public XWindow granResponsePrefab;
    public RectTransform canvas;

    public int CIAToGrandmaCount;
    public int CIAToCIACount;
    public int CIAToIRCount;
    public int CIAMax;

    public IncomingCallWindow incomingCallPrefab;

    public string[] Results = {
        "Fatally Inept",
        "Adequate",
        "Goodie Two-Shoes",
        "Sloppy",
        "Keyboard Monkey"
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
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.name == "StephensScene")
        {
            RunIntroSequence();
        }
    }

    public void HandleDrop (proper_destinations targetDestination, proper_destinations actualDestination)
    {
        Debug.Log("GameManager.HandleDrop()");
        XWindow granResponse = Instantiate(granResponsePrefab, canvas);
        granResponse.OnClose += GranResponse_OnClose;
        granResponse.Maximize(Vector2.zero, Vector2.zero);

    }

    private void GranResponse_OnClose(object sender, System.EventArgs e)
    {
        GameObject.FindObjectOfType<FileSpawner>().Spawn();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void RunIntroSequence()
    {
        IncomingCallWindow incomingCall = Instantiate(incomingCallPrefab);
        incomingCall.transform.SetParent(GameObject.Find("Canvas").transform);
        incomingCall.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
