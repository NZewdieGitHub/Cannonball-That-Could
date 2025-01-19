using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string musicPref = "musicPref";
    private static readonly string oceanPref = "oceanPref";
    private int firstPlayInt;
    private float musicFloat = 1f, oceanFloat = 1f;
    // arrays for music and sound effects
    [SerializeField]
    public Sound[] sounds;
    public AudioSource[] audioSources;
    
    [SerializeField]
    public Slider volumeSlider, oceanSlider;

   
    AudioSource oceanSource;
    // Volume fields
    
    AudioManager audioManager;
    public bool musicMuted = false;
    [SerializeField]
    public AudioSource musicAudio;
    [SerializeField]
    public AudioSource oceanAudio;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        // If player plays for the first time
        //if (firstPlayInt == 0)
        //{
            //musicFloat = 0.5f;
            //oceanFloat = 0.5f;

            // Check if scenes have Player Prefs
            if (!PlayerPrefs.HasKey(musicPref))
            {
            
                PlayerPrefs.SetFloat(musicPref, musicFloat);
            }
            else
            {
                 // pull from player prefs
                musicFloat = PlayerPrefs.GetFloat(musicPref);
                volumeSlider.value = musicFloat;
            }
            if (!PlayerPrefs.HasKey(oceanPref)) 
            {
                PlayerPrefs.SetFloat(oceanPref, oceanFloat);
            }
            else
            {
                // pull from player prefs
                oceanFloat = PlayerPrefs.GetFloat(oceanPref);
                oceanSlider.value = oceanFloat;
            }
          
    }

    /// <summary>
    /// Play sound
    /// </summary>
    /// <param name="name"></param>
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    /// <summary>
    /// Change volume
    /// </summary>
    public void ChangeVolume()
    {
        musicAudio.volume = volumeSlider.value;
    }

    /// <summary>
    /// Change volume
    /// </summary>
    public void ChangeOceanVolume()
    {
        oceanAudio.volume = oceanSlider.value;

    }

    /// <summary>
    /// Save music settings
    /// </summary>
    public void SaveSoundSettings() 
    {
        // save music
        PlayerPrefs.SetFloat(musicPref, volumeSlider.value);
        PlayerPrefs.SetFloat(oceanPref, oceanSlider.value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Save the volume values if window loses focus
    /// </summary>
    /// <param name="inFocus"></param>
    void OnApplicationFocus(bool inFocus)
    {
        // if the user is interacting with the window
        if (!inFocus)
        {
            // save sound settings
            SaveSoundSettings();
        }
    }
    /// <summary>
    /// Updates the sound
    /// </summary>
    public void UpdateSound()
    {
        // Have the slider match the volume level
        musicAudio.volume = volumeSlider.value;
        oceanAudio.volume = oceanSlider.value;
    }
}

