using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(LoadBattle());
    }

    IEnumerator LoadBattle() {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }

}
