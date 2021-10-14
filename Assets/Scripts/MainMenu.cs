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
	Animator unavailableAnimator;
	
	public void Start() {
		mainAnimator = main.GetComponent<Animator>();
		creditsAnimator = credits.GetComponent<Animator>();
		titleAnimator = title.GetComponent<Animator>();
		unavailableAnimator = notAvailable.GetComponent<Animator>();
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
		StartCoroutine(DisableMain(0.5f));
		notAvailable.SetActive(true);
	}
	
	public void CreditsButton() {
		StartCoroutine(DisableTitle(0f));
		StartCoroutine(DisableMain(0.5f));
		credits.SetActive(true);
	}
	
	public void CreditsBack() {
		StartCoroutine(DisableCredits(0.4f));
		title.SetActive(true);
		main.SetActive(true);
		
	}
    
    public void ExitButton() {
		Application.Quit();
    }
    
	public void UnavailableBack() {
		StartCoroutine(cUnailableBack(0.7f));
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
	
	IEnumerator cUnailableBack(float delay) {
		unavailableAnimator.SetTrigger("Close");
		yield return new WaitForSeconds(delay);
		notAvailable.SetActive(false);
		main.SetActive(true);
	}
}
