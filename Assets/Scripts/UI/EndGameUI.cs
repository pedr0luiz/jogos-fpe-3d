using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour {
    
    public Text game_status_placeholder;
    private GameManager gm;
    
    // Start is called before the first frame update
    void Start() {
        if (gm == null) {
            gm = GameManager.GetInstance();
        }
        
        set_ui();
    }

    void set_ui() {
        string game_status = "You Lost!";
        if (gm.player_won) {
            game_status = "You Won!";
        }

        game_status_placeholder.text = game_status;
    }

    public void GotToMainMenu() {
        gm.ResetGame();
    }
}
