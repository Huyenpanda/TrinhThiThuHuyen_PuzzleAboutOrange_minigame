using Hyb.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ManualSingletonMono<AudioManager>
{
    public AudioSource sfxSource;
    public AudioSource musicSource;

    //SFX
    public AudioClip button;
    public AudioClip die;

    //BG

    private void Start()
    {
        this.sfxSource.mute = PlayerPrefs.GetInt("Muted", 0) == 1;
        this.musicSource.mute = PlayerPrefs.GetInt("Muted", 0) == 1;
    }
}
