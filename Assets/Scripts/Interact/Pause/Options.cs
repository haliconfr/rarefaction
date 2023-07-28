using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public AudioSource music;
    public Slider musicSlider, sfxSlider;
    public AudioMixer sfxMixer, musicMixer;

    void Start(){
        music.Play();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
        	string option = resolutions[i].width + " x " + 
            resolutions[i].height;
        	options.Add(option);
        	if (resolutions[i].width == Screen.currentResolution.width 
                  && resolutions[i].height == Screen.currentResolution.height)
        	currentResolutionIndex = i;
        }
        options = options.Distinct().ToList();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
        musicSlider.value = PlayerPrefs.GetFloat("musVolume", 0);
    }
    public void SetSfx(float volume)
    {
        Debug.Log("got here");
    	PlayerPrefs.SetFloat("sfxVolume", volume);
    	sfxMixer.SetFloat("sfxVolume", sfxSlider.value);
        Debug.Log(volume);
    }
    public void SetMusic(float volume)
    {
    	PlayerPrefs.SetFloat("musVolume", volume);
    	musicMixer.SetFloat("MusicVolume", volume);
        Debug.Log(volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
    	Screen.fullScreen = isFullscreen;
        if(isFullscreen == true){
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        if(isFullscreen == false){
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }
    public void SetResolution(int resolutionIndex)
    {
    	Resolution resolution = resolutions[resolutionIndex];
    	Screen.SetResolution(resolution.width, 
        resolution.height, Screen.fullScreen);
    }
}
