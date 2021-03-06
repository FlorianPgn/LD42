﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip mainTheme;
    public AudioClip menuTheme;

    public static MusicManager instance;

    // Use this for initialization
    void Start () {
        instance = this;
	}

    public void PlayMainTheme()
    {
        AudioManager.instance.PlayMusic(mainTheme, 3);
    }

    public void PlayMenuTheme()
    {
        AudioManager.instance.PlayMusic(menuTheme, 2);
    }
}
