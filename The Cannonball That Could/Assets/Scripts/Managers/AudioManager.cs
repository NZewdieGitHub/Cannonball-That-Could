using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    // arrays for music and sound effects
    [SerializeField]
    public Sound[] sounds;

    
    [SerializeField]
    public Slider volumeSlider;

    [SerializeField]
    MenuManager menuManager;
    AudioSource oceanSource;
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
        Play("Waves");
    }

    // Update is called once per frame
    void Update()
    {
        

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
    public void ChangeOceanVolume()
    {
        oceanSource.volume = volumeSlider.value;
        
    }
}

