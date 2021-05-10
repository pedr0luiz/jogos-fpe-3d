using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControlScene : MonoBehaviour {
    public void return_to_main_menu() {
        Debug.Log("AQUI");
        SceneManager.LoadScene("MainMenuScene");
    }

}
