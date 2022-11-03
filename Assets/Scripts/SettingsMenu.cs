using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;

	public Slider musicSlider;
	public Slider soundEffectsSlider;

	public Dropdown resolutionDropDown;

	Resolution[] resolutions;

	public void Start()
	{
		resolutions = resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
		resolutionDropDown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + "x" + resolutions[i].height;
			options.Add(option);

			if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropDown.AddOptions(options);
		resolutionDropDown.value = currentResolutionIndex;
		resolutionDropDown.RefreshShownValue();

		Screen.fullScreen = true;

		audioMixer.GetFloat("Music", out float musicValueForSlider);
		musicSlider.value = musicValueForSlider;

		audioMixer.GetFloat("SoundEffects", out float soundEffectsValueForSlider);
		soundEffectsSlider.value = soundEffectsValueForSlider;
	}

	public void SetMusicVolume()
	{
		audioMixer.SetFloat("Music", musicSlider.value);
	}
	public void SetSoundVolume()
	{
		audioMixer.SetFloat("SoundEffects", soundEffectsSlider.value);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = true;
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
}
