using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    public bool player_won = false;
    public bool has_knife = false;
    public bool has_multimetro = false;
    public bool has_radio = false;
    public bool game_paused = false;

    public bool bomb_hint_opened = false;
    public bool has_read_hint = false;

    public bool has_read_controlls = false;


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

    public void got_multimetro(){
        has_multimetro = true;
    }

    public void got_radio(){
        has_radio = true;
    }

    public bool can_defuse(){
        return has_knife && has_multimetro && has_radio;
    }

    public void toogle_pause_game() {
        game_paused = !game_paused;
    }

    public void set_bomb_hint(bool state){
        if(state) has_read_hint = true;
        bomb_hint_opened = state;
    }

    public void reset(){
        has_knife = false;
        has_multimetro = false;
        has_radio = false;
        game_paused = false;
        bomb_hint_opened = false;
        has_read_hint = false;
    }

}
