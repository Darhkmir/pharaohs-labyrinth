using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	
	public AudioMixer audioMixer;
	public Dropdown resolutionDropdown;
	public Dropdown graphicsDropdown;
	public Toggle fullscreenToggle;
	public Slider volumeSlider;
	
	public Resolution[] resolutions;
	
	void Start() {
		GetPlayerPrefs();
		
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();
		
		List<string> options = new List<string>();
		
		int currentResolutionIndex = 0;
		
		for (int i = 0; i < resolutions.Length; i++) 
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);
			
			if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
				currentResolutionIndex = i;
			}
		}
		
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}
    
	public void SetResolution (int resolutionIndex) {
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		PlayerPrefs.SetInt("resWidth", resolution.width);
		PlayerPrefs.SetInt("resHeight", resolution.height);
	}
    
	public void SetVolume (float volume) {
		audioMixer.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("volume", volume);
	}
	
	public void SetQuality (int qualityIndex) {
		QualitySettings.SetQualityLevel(qualityIndex);
		PlayerPrefs.SetInt("quality", qualityIndex);
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
			PlayerPrefs.SetInt("resWidth", 1280);
			PlayerPrefs.SetInt("resHeight", 720);
			Screen.SetResolution(1280, 720, Screen.fullScreen);
			
			PlayerPrefs.SetFloat("volume", 0f);
			audioMixer.SetFloat("volume", 0f);
			volumeSlider.value = 0f;
			
			PlayerPrefs.SetInt("quality", 2);
			QualitySettings.SetQualityLevel(2);
			graphicsDropdown.value = 2;
			
			PlayerPrefs.SetString("fullscreen", "false");
			Screen.fullScreen = false;
			fullscreenToggle.isOn = false;
		} else {
			int prefsResWidth = PlayerPrefs.GetInt("resWidth");
			int prefsResHeight = PlayerPrefs.GetInt("resHeight");
			Screen.SetResolution(prefsResWidth, prefsResHeight, Screen.fullScreen);
			
			float prefsVolume = PlayerPrefs.GetFloat("volume");
			audioMixer.SetFloat("volume", prefsVolume);
			volumeSlider.value = prefsVolume;
			
			int prefsQuality = PlayerPrefs.GetInt("quality");
			QualitySettings.SetQualityLevel(prefsQuality);
			graphicsDropdown.value = prefsQuality;
			
			bool prefsFullscreen = PlayerPrefs.GetString("fullscreen") == "true";
			Screen.fullScreen = prefsFullscreen;
			fullscreenToggle.isOn = prefsFullscreen;
		}
	}
}
