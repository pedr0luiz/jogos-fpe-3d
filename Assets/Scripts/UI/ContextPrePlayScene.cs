using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContextPrePlayScene : MonoBehaviour {
    public void play_game() {
        Debug.Log("AQUI");
        SceneManager.LoadScene("PlayScene");
    }

}
