using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private GameObject tutorialPanel;

    public void CloseTutorial() {
        PlayerPrefs.SetInt("tutorial", 1);
    }
    
    void Start() {
        if (PlayerPrefs.HasKey("tutorial")) {
            tutorialPanel.SetActive(false);
        }
    }

}
