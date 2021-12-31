using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropToNextFloor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Player")) {
            int sceneID = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneID + 1);
        }
    }

}
