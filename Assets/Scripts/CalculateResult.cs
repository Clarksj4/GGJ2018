using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateResult : MonoBehaviour {

    Text resultText;
    
    void Awake()
    {
        resultText = GetComponent<Text>();
    }
    
	void Start () {
		GameMaster gameMaster = GameMaster.gameMaster;
	}
	
	void Update () {
		
	}
}
