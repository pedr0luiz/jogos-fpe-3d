using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour {

    // Code snippet by https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html
    private GameObject[] other;
    private bool NotFirst = false;

    private void Awake() {

        other = GameObject.FindGameObjectsWithTag("Music");
 
        foreach (GameObject oneOther in other) {
            if (oneOther.scene.buildIndex == -1) {
                NotFirst = true;
            }
        }

        if (NotFirst == true) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
