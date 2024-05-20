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

    public static AudioManager instance;


    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 3f)]
        public float volume;
        [Range(0f, 3f)]
        public float pitch;
        public bool isMusic;
        public bool isUISound;
        public bool isSoundFX;

        [HideInInspector]
        public AudioSource source;
        public float originalVolume;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en AudioManager");
            return;
        }
        instance = this;

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
            s.source.pitch = s.pitch;

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

            if (s.name == "TeslaTower")
            {
                s.source.loop = true;
            }
        }
        PlayMusic();
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.pitch = s.pitch; 
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void PlayMusic()
    {
        Sound music = Array.Find(sounds, sound => sound.isMusic);
        if (music != null)
        {
            music.source.loop = true;
            music.source.pitch = music.pitch;
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
            s.source.pitch = s.pitch; 
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    public void PlaySoundEffect(string soundName, float pitch = 1.0f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName && !sound.isMusic && !sound.isUISound);
        if (s != null)
        {
            Debug.Log("Sound effect played");
            s.source.pitch = pitch; 
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
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

