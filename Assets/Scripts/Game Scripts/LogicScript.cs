using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public int playerHighscore;
    public GameObject playerJet;
    public SceneSetterScrpit sceneSetterScrpit;
    public Text scoreTextField;
    public Text highscoreTextField;
    void Start(){
        playerHighscore = PlayerPrefs.GetInt("highscore");
        updateHighscore();
    }
    void Update(){}

    private void updateScore(){
        scoreTextField.text = playerScore.ToString();
    }

    private void updateHighscore(){
        highscoreTextField.text = "Highscore: " + playerHighscore.ToString();
    }


    [ContextMenu("Increase Score")]
    public void addScore(int val){
        playerScore += val;
        updateScore();

        if(playerScore <= playerHighscore) return;
        playerHighscore = playerScore;
        PlayerPrefs.SetInt("highscore", playerHighscore);
        updateHighscore();
    }
    public void resetScore(){
        playerScore = 0;
        updateScore();
    }
    public void gameOver(){
        sceneSetterScrpit.displayGameOverScreen();
        playerJet.GetComponent<Rigidbody2D>().simulated = false;
    }
    public void restartGame(){
        playerJet.GetComponent<Rigidbody2D>().simulated = true;
        sceneSetterScrpit.restartGame();

        playerScore = 0;
        playerHighscore = PlayerPrefs.GetInt("highscore");
        updateScore();
        updateHighscore();
    }
}