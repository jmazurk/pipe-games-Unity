using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetterScrpit : MonoBehaviour
{
    public GameObject gameOverScreen;

    void Start(){
        gameOverScreen.SetActive(false);
    }
    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverScreen.SetActive(false);
    }
    public void displayGameOverScreen(){
        gameOverScreen.SetActive(true);
    }
}
