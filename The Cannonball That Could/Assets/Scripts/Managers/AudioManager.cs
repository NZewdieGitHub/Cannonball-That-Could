using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // arrays for music and sound effects
    [SerializeField]
    public Sound[] musicSounds, sfxSounds;
    [SerializeField]
    public AudioSource musicSource, sfxScource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("BattleTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Play Specific Music
    /// </summary>
    /// <param name="name"></param>
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        // Search for parameter sound
        if (s == null)
        {
            Debug.Log("Sound Not Found");      
        }
        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }
    /// <summary>
    /// Play Specific Music
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        // Search for parameter sound
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxScource.PlayOneShot(s.audioClip);
        }
    }
}

