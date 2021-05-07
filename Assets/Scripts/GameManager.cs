using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    public bool player_won = false;


    private static GameManager _instance;
    public static GameManager GetInstance() {
        if(_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }

    public void EndGame() {
        SceneManager.LoadScene("EndGameScene");
    }

    public void ResetGame() {
        player_won = false;
        SceneManager.LoadScene("MainMenuScene");
    }

}
