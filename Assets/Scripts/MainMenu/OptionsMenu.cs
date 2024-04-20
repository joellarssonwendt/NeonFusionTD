using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject options;
    public Slider musicVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Slider UISoundEffectsVolumeSlider;
    public AudioManager audioManager;

    private void Start()
    {
        options.SetActive(false);

        musicVolumeSlider.value = audioManager.MusicVolume;
        soundEffectsVolumeSlider.value = audioManager.SoundEffectsVolume;
        UISoundEffectsVolumeSlider.value = audioManager.UISoundEffectsVolume;
    }

    public void OnMusicVolumeChanged(float value)
    {
        audioManager.MusicVolume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateMusicVolume();
    }

    public void OnSoundEffectsVolumeChanged(float value)
    {
        audioManager.SoundEffectsVolume = value;

        PlayerPrefs.SetFloat("SoundEffectsVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateSoundEffectsVolume();
    }

    public void OnUISoundVolumeChanged(float value)
    {
        audioManager.UISoundEffectsVolume = value;

        PlayerPrefs.SetFloat("UISoundEffectsVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateUISoundEffectsVolume();
    }
    public void UpdateSliders()
    {
        musicVolumeSlider.value = audioManager.MusicVolume;
        soundEffectsVolumeSlider.value = audioManager.SoundEffectsVolume;
        UISoundEffectsVolumeSlider.value = audioManager.UISoundEffectsVolume;
    }
}
