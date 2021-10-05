using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public GameObject canvas;
	public GameObject main;
	public GameObject credits;
	public GameObject notAvailable;
	
	Animator mainAnimator;
	Animator creditsAnimator;
	
	public void Start() {
		mainAnimator = main.GetComponent<Animator>();
		creditsAnimator = credits.GetComponent<Animator>();
	}
	
	public void CreditsButton() {
		StartCoroutine(DisableMain(0.4f));
		credits.SetActive(true);
	}
	
	public void CreditsBackButton() {
		StartCoroutine(DisableCredits(0.5f));
		main.SetActive(true);
		
	}
    
    public void ExitButton() {
		Application.Quit();
    }
	
	IEnumerator DisableMain(float delay) {
		mainAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		main.SetActive(false);
	}
	
	IEnumerator DisableCredits(float delay) {
		creditsAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		credits.SetActive(false);
	}
}
