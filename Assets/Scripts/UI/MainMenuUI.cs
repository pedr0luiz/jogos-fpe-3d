using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    public void play_game() {
        SceneManager.LoadScene("ContextPrePlayScene");
    }

    public void game_control_scene() {
        SceneManager.LoadScene("ControlScene");
    }

    public void sound_control_scene() {
        SceneManager.LoadScene("SoundControlScene");
    }
}
