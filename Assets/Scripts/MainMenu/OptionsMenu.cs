using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject options;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Slider UISoundEffectsVolumeSlider;
    public Toggle masterVolumeToggle;
    public Toggle musicVolumeToggle;
    public Toggle soundEffectsVolumeToggle;
    public Toggle UISoundEffectsVolumeToggle;
    public AudioManager audioManager;

    private void OnEnable()
    {
        UpdateSliders();
    }

    private void Start()
    {
        options.SetActive(false);

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5f);
        UISoundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("UISoundEffectsVolume", 0.5f);

        masterVolumeToggle.isOn = PlayerPrefs.GetInt("MasterVolumeMute", 0) == 1;
        musicVolumeToggle.isOn = PlayerPrefs.GetInt("MusicVolumeMute", 0) == 1;
        soundEffectsVolumeToggle.isOn = PlayerPrefs.GetInt("SoundEffectsVolumeMute", 0) == 1;
        UISoundEffectsVolumeToggle.isOn = PlayerPrefs.GetInt("UISoundEffectsVolumeMute", 0) == 1;

        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundEffectsVolumeSlider.onValueChanged.AddListener(OnSoundEffectsVolumeChanged);
        UISoundEffectsVolumeSlider.onValueChanged.AddListener(OnUISoundVolumeChanged);

        masterVolumeToggle.onValueChanged.AddListener(OnMasterVolumeToggleChanged);
        musicVolumeToggle.onValueChanged.AddListener(OnMusicVolumeToggleChanged);
        soundEffectsVolumeToggle.onValueChanged.AddListener(OnSoundEffectsVolumeToggleChanged);
        UISoundEffectsVolumeToggle.onValueChanged.AddListener(OnUISoundEffectsVolumeToggleChanged);
    }

    public void OnMasterVolumeChanged(float value)
    {
        audioManager.MasterVolume = value;

        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateMasterVolume();

        if (masterVolumeToggle.isOn && value > 0)
        {
            masterVolumeToggle.isOn = false;
        }
    }

    public void OnMusicVolumeChanged(float value)
    {
        audioManager.MusicVolume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateMusicVolume();

        if (musicVolumeToggle.isOn && value > 0)
        {
            musicVolumeToggle.isOn = false;
        }
    }

    public void OnSoundEffectsVolumeChanged(float value)
    {
        audioManager.SoundEffectsVolume = value;

        PlayerPrefs.SetFloat("SoundEffectsVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateSoundEffectsVolume();

        if (soundEffectsVolumeToggle.isOn && value > 0)
        {
            soundEffectsVolumeToggle.isOn = false;
        }
    }

    public void OnUISoundVolumeChanged(float value)
    {
        audioManager.UISoundEffectsVolume = value;

        PlayerPrefs.SetFloat("UISoundEffectsVolume", value);
        PlayerPrefs.Save();

        audioManager.UpdateUISoundEffectsVolume();

        if (UISoundEffectsVolumeToggle.isOn && value > 0)
        {
            UISoundEffectsVolumeToggle.isOn = false;
        }
    }
    public void UpdateSliders()
    {
        masterVolumeSlider.value = audioManager.MasterVolume;
        musicVolumeSlider.value = audioManager.MusicVolume;
        soundEffectsVolumeSlider.value = audioManager.SoundEffectsVolume;
        UISoundEffectsVolumeSlider.value = audioManager.UISoundEffectsVolume;
    }

    public void OnMasterVolumeToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MasterVolumeMute", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isOn)
        {
            audioManager.MasterVolume = 0;
            audioManager.UpdateMasterVolume();
        }
        else
        {
            audioManager.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            audioManager.UpdateMasterVolume();
        }
    }

    public void OnMusicVolumeToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MusicVolumeMute", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isOn)
        {
            audioManager.MusicVolume = 0;
            audioManager.UpdateMusicVolume();
        }
        else
        {
            audioManager.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            audioManager.UpdateMusicVolume();
        }
    }

    public void OnSoundEffectsVolumeToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SoundEffectsVolumeMute", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isOn)
        {
            audioManager.SoundEffectsVolume = 0;
            audioManager.UpdateSoundEffectsVolume();
        }
        else
        {
            audioManager.SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5f);
            audioManager.UpdateSoundEffectsVolume();
        }
    }



    public void OnUISoundEffectsVolumeToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("UISoundEffectsVolumeMute", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isOn)
        {
            audioManager.UISoundEffectsVolume = 0;
            audioManager.UpdateUISoundEffectsVolume();
        }
        else
        {
            audioManager.UISoundEffectsVolume = PlayerPrefs.GetFloat("UISoundEffectsVolume", 0.5f);
            audioManager.UpdateUISoundEffectsVolume();
        }
    }
}
