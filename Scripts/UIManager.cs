using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int MaxScore;

    private int playerScore = 0;
    private int cpuScore = 0;

    private GameObject gameResult;
    private GameObject postGameMessage;

    public bool hasEnded { get; set; }

	// Use this for initialization
	void Start () {
        gameResult = GameObject.FindGameObjectWithTag("GameResult");
        postGameMessage = GameObject.FindGameObjectWithTag("PostGameMessage");

        gameResult.SetActive(false);
        postGameMessage.SetActive(false);

        hasEnded = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreasePlayerScore(){
        playerScore++;
        GameObject.FindGameObjectWithTag("PlayerScore").GetComponent<Text>().text = "Player Score: " + playerScore;
        if(playerScore >= MaxScore){
            gameResult.GetComponent<Text>().text = "You win!";
            gameResult.SetActive(true);
            postGameMessage.SetActive(true);
            hasEnded = true;
        }
    }

    public void IncreaseCPUScore(){
        cpuScore++;
        GameObject.FindGameObjectWithTag("CPUScore").GetComponent<Text>().text = "CPU Score: " + cpuScore;
        if(cpuScore >= MaxScore){
            gameResult.GetComponent<Text>().text = "You lose!";
            gameResult.SetActive(true);
            postGameMessage.SetActive(true);
            hasEnded = true;
        }

    }

    public void StartNewGame(){
        SceneManager.LoadScene("Game");
    }

}
