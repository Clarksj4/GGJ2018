using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using System;
using UnityEngine.UI;
using System.Linq;

public class GameMaster : MonoBehaviour {

    public static GameMaster gameMaster;
    public SceneManager sceneManager;
    public XWindow granResponsePrefab;
    public XWindow ciaResponsePrefab;
    public XWindow intendedResponsePrefab;
    public RectTransform canvas;

    public int CIAToGrandmaCount;
    public int CIAToCIACount;
    public int CIAToIRCount;
    public int CIAMax;

    public int messageCounter;
    public int totalMessageNumberTarget;

    public IncomingCallWindow incomingCallPrefab;

    public string[] Results = {
        "Fatally Inept",
        "Adequate",
        "Goodie Two-Shoes",
        "Sloppy",
        "Keyboard Monkey"
    };

    public List<string>[,] Responses;
    
	void Awake()
    {
        CreateResponses();

        if (gameMaster == null)
        {
            gameMaster = this;
        }
        else if (gameMaster != this)
        {
            Destroy(this.gameObject);
        }

        
        sceneManager = new SceneManager();
        GameObject fs = GameObject.Find("FileSpawner");
        
        if (fs != null)
        {
            List<content> contents = fs.GetComponent<FileSpawner>().content_items;
            totalMessageNumberTarget = contents.Count;
            foreach (content c in contents)
            {
                if (c.proper_destination == proper_destinations.cia)
                {
                    CIAMax++;
                }
            }
        }
        else
        {
            totalMessageNumberTarget = 0;
        }
       
        
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

        XWindow responseWindow;

        switch (actualDestination)
        {
            case proper_destinations.grandma:
                responseWindow = Instantiate(granResponsePrefab, canvas);
                break;
            case proper_destinations.cia:
                responseWindow = Instantiate(ciaResponsePrefab, canvas);
                break;
            default:
                // Special case: ProperDestination = CIA, Actual destination you sent it to = 'Intended Person'
                if (targetDestination == proper_destinations.cia)
                    responseWindow = Instantiate(ciaResponsePrefab, canvas);

                // Special case: ProperDestination = Gran, Acutal destination you sent it to = 'Intended Person'
                else if (targetDestination == proper_destinations.grandma)
                    responseWindow = Instantiate(granResponsePrefab, canvas);
                else
                    responseWindow = Instantiate(intendedResponsePrefab, canvas);
                break;
        }

        responseWindow.OnClose += ResponseWindow_OnClose;
        responseWindow.Maximize(Vector2.zero, Vector2.zero);

        List<string> responsePool = Responses[(int)targetDestination, (int)actualDestination];
        int index = UnityEngine.Random.Range(0, responsePool.Count);
        responseWindow.ResponseText.text = responsePool[index];

        if (targetDestination == proper_destinations.cia)
        {
            if (actualDestination == proper_destinations.cia)
            {
                CIAToCIACount++;
            }
            else if (actualDestination == proper_destinations.grandma)
            {
                CIAToGrandmaCount++;
            }
            else if (actualDestination == proper_destinations.intended)
            {
                CIAToIRCount++;
            }
        }

        messageCounter++;
        

    }

    private void ResponseWindow_OnClose(object sender, System.EventArgs e)
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

    private void CreateResponses()
    {
        int nDestinations = Enum.GetNames(typeof(proper_destinations)).Length;
        Responses = new List<string>[nDestinations, nDestinations];

        Responses[(int)proper_destinations.grandma, (int)proper_destinations.grandma] = new List<string>()
        {
            "My, oh my! What a delightful surprise!",
            "It's wonderful to hear from you, dearie!",
            "Isn't the interweb a marvelous place?",
            "This reminds me of your grandpa.",
            "I love this one!",
            "Oh you have such exquisite taste, my dear!",
            "This makes the pain of withering away slightly more bearable."
        };

        Responses[(int)proper_destinations.intended, (int)proper_destinations.grandma] = new List<string>()
        {
            "I don't understandï¿½",
            "I hope this isn't one of those scam E-mails.",
            "People like this make me feel uncomfortable.",
            "The World Wide Webernet is a strange and scary place.",
            "You can't expect a poor old grandma like me to understand these obscure ï¿½memesï¿½.",
            "Dearie, I think something's wrong with my E-mails."
        };

        Responses[(int)proper_destinations.cia, (int)proper_destinations.grandma] = new List<string>()
        {
            "My poor old heart can't take this horrific imagery!",
            "I don't feel like this was meant for meï¿½",
            "What ?",
            "Is thisï¿½ Illegal ?",
            "My dentures fell out.",
            "Did you get those cookies I sent in the mail ?",
            "Dearie, I think some hackers are using my E-Mail for their criminal activities!"
        };

        Responses[(int)proper_destinations.grandma, (int)proper_destinations.intended] = new List<string>()
        {
            "How is your new job going, dear? I understand you must be very busy, but when you’ve got a moment I’d love to hear from you!"
        };

        Responses[(int)proper_destinations.intended, (int)proper_destinations.intended] = new List<string>()
        {
            "Your interests do not stray into the realm of absurdity.",
            "That one required no second thought.",
            "Sometimes it's better not to ask.",
            "Curiosity killed the cat, afterall.",
            "I dont think the CIA would have appreciated that one.",
            "Wasting time with irrelevant diversions is not on your agenda.",
            "No time for nonsense.",
            "I'm sure whoever that was intended for is super stoked with you.",
            "You are a benevolent guardian of the World Wide Webernet"
        };

        Responses[(int)proper_destinations.cia, (int)proper_destinations.intended] = new List<string>()
        {
            "Hey it's your boy from the CIA, remember? Don't forget: if you happen upon any juicy scoops, send them straight to us!"
        };

        Responses[(int)proper_destinations.grandma, (int)proper_destinations.cia] = new List<string>()
        {
            "Cute. I hate cute.",
            "Fucking really?",
            "I don't understand what you expected me to do with this.",
            "I feel like this is better suited for someone who plays lawn bowls than the C - I - fucking - A.",
            "Why ? Justï¿½ why ?",
            "You do realise we are the most highly regarded intelligence agency in the world?",
            "Hey, I've got a good idea: why don't you go fuck yourself ?"
        };

        Responses[(int)proper_destinations.intended, (int)proper_destinations.cia] = new List<string>()
        {
            "Why are you wasting our time with this drivel?",
            "BOOORINNGGG",
            "I donï¿½t get paid enough for this",
            "Fucking useless",
            "I know itï¿½s your first day butï¿½ really ?",
            "This couldnï¿½t be less relevant to my job.",
            "Having fun eavesdropping on the entire world ?"
        };

        Responses[(int)proper_destinations.cia, (int)proper_destinations.cia] = new List<string>()
        {
            "Good shit. We're going to bring these fuckers down.",
            "You're a goddamn hero, kid.",
            "SWAT teams dispatched.",
            "Wow, you actually got something! I don't goddamn believe it.",
            "Maybe you're not so useless after all.",
            "We'll take it from here.",
            "I would give you a promotion, but you don't get paid for this.",
            "You just made the world a better place.Not that you're ever allowed to go outside."
        };
    }

    void RunIntroSequence()
    {
        IncomingCallWindow incomingCall = Instantiate(incomingCallPrefab);
        incomingCall.transform.SetParent(GameObject.Find("Canvas").transform);
        incomingCall.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
