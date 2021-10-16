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
	public GameObject options;
	
	Animator mainAnimator;
	Animator creditsAnimator;
	Animator titleAnimator;
	Animator unavailableAnimator;
	Animator optionsAnimator;
	
	public void Start() {
		mainAnimator = main.GetComponent<Animator>();
		creditsAnimator = credits.GetComponent<Animator>();
		titleAnimator = title.GetComponent<Animator>();
		unavailableAnimator = notAvailable.GetComponent<Animator>();
		optionsAnimator = options.GetComponent<Animator>();
	}
	
	public void NewButton() {
		StartCoroutine(DisableMain(0.5f));
		notAvailable.SetActive(true);
	}
	
	public void LoadButton() {
		StartCoroutine(DisableMain(0.5f));
		notAvailable.SetActive(true);
	}
	
	public void OptionsButton() {
		StartCoroutine(DisableTitle(0.5f));
		StartCoroutine(DisableMain(0.5f));
		options.SetActive(true);
	}
	
	public void CreditsButton() {
		StartCoroutine(DisableTitle(0.5f));
		StartCoroutine(DisableMain(0.5f));
		credits.SetActive(true);
	}
    
    public void ExitButton() {
		Application.Quit();
    }
    
	public void CreditsBack() {
		StartCoroutine(DisableCredits(0.4f));
		title.SetActive(true);
		main.SetActive(true);
		
	}
    
	public void UnavailableBack() {
		StartCoroutine(cUnailableBack(0.7f));
	}
	
	public void OptionsBack() {
		StartCoroutine(DisableOptions(0.7f));
		title.SetActive(true);
		main.SetActive(true);
	}
	
	IEnumerator DisableMain(float delay) {
		mainAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		main.SetActive(false);
	}
	
	IEnumerator DisableTitle(float delay) {
		titleAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		title.SetActive(false);
	}
	
	IEnumerator DisableCredits(float delay) {
		creditsAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		credits.SetActive(false);
	}
	
	IEnumerator DisableOptions(float delay) {
		optionsAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		options.SetActive(false);
	}
	
	IEnumerator cUnailableBack(float delay) {
		unavailableAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		notAvailable.SetActive(false);
		main.SetActive(true);
	}
}
