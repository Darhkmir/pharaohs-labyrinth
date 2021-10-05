using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public GameObject canvas;
	public GameObject main;
	public GameObject credits;
	public GameObject notAvailable;
	public GameObject title;
	
	Animator mainAnimator;
	Animator creditsAnimator;
	Animator titleAnimator;
	
	public void Start() {
		mainAnimator = main.GetComponent<Animator>();
		creditsAnimator = credits.GetComponent<Animator>();
		titleAnimator = title.GetComponent<Animator>();
	}
	
	public void CreditsButton() {
		StartCoroutine(DisableMain(0.5f));
		credits.SetActive(true);
	}
	
	public void CreditsBackButton() {
		StartCoroutine(DisableCredits(0.4f));
		title.SetActive(true);
		main.SetActive(true);
		
	}
    
    public void ExitButton() {
		Application.Quit();
    }
	
	IEnumerator DisableMain(float delay) {
		titleAnimator.SetTrigger("Close");
		mainAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		main.SetActive(false);
		title.SetActive(false);
	}
	
	IEnumerator DisableCredits(float delay) {
		creditsAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		credits.SetActive(false);
	}
}
