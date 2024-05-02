using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float MasterVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float SoundEffectsVolume = 0.5f;
    public float UISoundEffectsVolume = 0.5f;

    public bool isMasterVolumeMuted = false;
    public bool isMusicVolumeMuted = false;
    public bool isSoundEffectsVolumeMuted = false;
    public bool isUISoundEffectsVolumeMuted = false;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        public bool isMusic;
        public bool isUISound;
        public bool isSoundFX;

        [HideInInspector]
        public AudioSource source;
        public float originalVolume;
    }

    void Awake()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); 
        SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5f);
        UISoundEffectsVolume = PlayerPrefs.GetFloat("UISoundEffectsVolume", 0.5f);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); 
            s.source.clip = s.clip;
            s.originalVolume = s.volume;
            s.source.volume = s.volume * MasterVolume;

            if (s.isMusic)
            {
                s.source.volume = s.volume * MusicVolume;
            }
            else if (s.isUISound)
            {
                s.source.volume = s.volume * UISoundEffectsVolume;
            }
            else if (s.isSoundFX)
            {
                s.source.volume = s.volume * SoundEffectsVolume;
            }
        }
        PlayMusic();
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayMusic()
    {
        Sound music = Array.Find(sounds, sound => sound.isMusic);
        if (music != null)
        {
            music.source.loop = true;
            music.source.Play();
        }
        else
        {
            Debug.LogWarning("No music track found.");
        }
    }

    public void PlayUISoundEffect(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName && !sound.isMusic);
        if (s != null)
        {
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    public void PlaySoundEffect(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName && !sound.isMusic && !sound.isUISound);
        if (s != null)
        {
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    public void UpdateMasterVolume()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = isMasterVolumeMuted ? 0 : s.volume * MasterVolume;
        }
    }

    public void UpdateMusicVolume()
    {
        Sound music = Array.Find(sounds, sound => sound.isMusic);
        if (music != null)
        {
            music.source.volume = isMusicVolumeMuted ? 0 : MusicVolume;
        }
    }

    public void UpdateUISoundEffectsVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.isUISound)
            {
                s.source.volume = isUISoundEffectsVolumeMuted ? 0 : UISoundEffectsVolume;
            }
        }
    }

    public void UpdateSoundEffectsVolume()
    {
        foreach (Sound s in sounds)
        {
            if (!s.isMusic && !s.isUISound)
            {
                s.source.volume = isSoundEffectsVolumeMuted ? 0 : SoundEffectsVolume;
            }
        }
    }
}

