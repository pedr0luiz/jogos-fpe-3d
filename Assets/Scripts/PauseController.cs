using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    private GameManager gm;
    public GameObject panel;

    void Start() {
        if (gm == null) {
            gm = GameManager.GetInstance();
        }
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            gm.toogle_pause_game();

            Debug.Log(gm.game_paused);
            if (gm.game_paused) {
                panel.SetActive(true);
            }
            else panel.SetActive(false);
            return;
        }
    }
}
