using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	
	public AudioMixer audioMixer;
	public Dropdown graphicsDropdown;
	public Toggle fullscreenToggle;
	public Slider volumeSlider;
	
	void Start() {
		GetPlayerPrefs();
	}
    
	public void SetVolume (float volume) {
		audioMixer.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("volume", volume);
	}
	
	public void SetFullscreen (bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
		if (isFullscreen == true) {
			PlayerPrefs.SetString("fullscreen", "true");
		} else {
			PlayerPrefs.SetString("fullscreen", "false");
		}
	}
	
	public void GetPlayerPrefs() {
		if (!PlayerPrefs.HasKey("volume")) {
			PlayerPrefs.SetFloat("volume", -30f);
			audioMixer.SetFloat("volume", -30f);
			volumeSlider.value = -30f;
			
			PlayerPrefs.SetString("fullscreen", "false");
			Screen.fullScreen = false;
			fullscreenToggle.isOn = false;
		} else {
			float prefsVolume = PlayerPrefs.GetFloat("volume");
			audioMixer.SetFloat("volume", prefsVolume);
			volumeSlider.value = prefsVolume;
			
			bool prefsFullscreen = PlayerPrefs.GetString("fullscreen") == "true";
			Screen.fullScreen = prefsFullscreen;
			fullscreenToggle.isOn = prefsFullscreen;
		}
	}
}
