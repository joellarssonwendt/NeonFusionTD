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
        audioManager.UpdateMusicVolume();
    }

    public void OnSoundVolumeChanged(float value)
    {
        audioManager.SoundEffectsVolume = value;
        audioManager.UpdateSoundEffectsVolume();
    }

    public void OnUISoundVolumeChanged(float value)
    {
        audioManager.UISoundEffectsVolume = value;
        audioManager.UpdateUISoundEffectsVolume();
    }
}
