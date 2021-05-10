using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    private GameManager gm;
    public GameObject panel;
    public GameObject bombDefuseHint;

    void Start() {
        if (gm == null) {
            gm = GameManager.GetInstance();
        }
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gm.bomb_hint_opened){
                gm.set_bomb_hint(false);
                bombDefuseHint.SetActive(false);
            } else{
                gm.toogle_pause_game();

                if (gm.game_paused) {
                    panel.SetActive(true);
                }
                else panel.SetActive(false);
            }
            return;
        }
    }
}
