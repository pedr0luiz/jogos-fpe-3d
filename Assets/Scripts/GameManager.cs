using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    public bool player_won = false;
    public bool has_knife = false;
    public bool game_paused = false;

    public bool bomb_hint_opened = false;
    public bool has_read_hint = false;


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

    public void got_knife(){
        has_knife = true;
    }

    public bool can_defuse(){
        return has_knife;
    }

    public void toogle_pause_game() {
        game_paused = !game_paused;
    }

    public void set_bomb_hint(bool state){
        if(state) has_read_hint = true;
        bomb_hint_opened = state;
    }

}
