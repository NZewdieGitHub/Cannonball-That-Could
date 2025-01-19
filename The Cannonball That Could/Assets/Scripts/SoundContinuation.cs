using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContinuation : MonoBehaviour
{
    private static readonly string musicPref = "musicPref";
    private static readonly string oceanPref = "oceanPref";
    private int firstPlayInt;
    private float musicFloat, oceanFloat;
    [SerializeField]
    public AudioSource musicAudio;
    [SerializeField]
    public AudioSource oceanAudio;
    // Start is called before the first frame update
    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(musicPref);
        oceanFloat = PlayerPrefs.GetFloat(oceanPref);

        oceanAudio.volume = oceanFloat;
        musicAudio.volume = musicFloat;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
