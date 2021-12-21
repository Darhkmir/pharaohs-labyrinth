using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
    
    public static int sceneID;
    
    void Start() {
        if (sceneID == 0) {
            StartCoroutine(ToSplash02());
        }
        if (sceneID == 1) {
            StartCoroutine(ToMainMenu());
        }
    }

    IEnumerator ToSplash02() {
        yield return new WaitForSeconds(3);
        sceneID = 1;
        SceneManager.LoadScene(1);
    }

    IEnumerator ToMainMenu() {
        yield return new WaitForSeconds(3);
        sceneID = 2;
        SceneManager.LoadScene(2);
    }

}
