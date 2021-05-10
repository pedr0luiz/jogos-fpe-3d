using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    GameManager gm;

        void Start()
    {
        if (gm == null) {
            gm = GameManager.GetInstance();
        }
    }
    public void play_game() {
        if(!gm.has_read_controlls){
            gm.has_read_controlls = true;
            SceneManager.LoadScene("ControlScene");
        } else{
            SceneManager.LoadScene("ContextPrePlayScene");
        }
    }

    public void game_control_scene() {
        SceneManager.LoadScene("ControlScene");
    }

    public void sound_control_scene() {
        SceneManager.LoadScene("SoundControlScene");
    }
}
