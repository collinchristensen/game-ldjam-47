using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public Camera gameCamera;

    public AudioClip[] hitHurtSounds;
    public AudioClip[] laserShootSounds;
    public AudioClip[] coinSounds;

    public AudioClip defeatSound;
    public AudioClip selectSound;

    public AudioClip gameMusic;
    public AudioClip menuMusic;


    public static AudioController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (instance == null)
            {
                GameObject go = new GameObject("AudioController");
                instance = go.AddComponent<AudioController>();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void PlayHurtSound()
    {

        AudioManager.instance.RandomizeSound(hitHurtSounds);

    }

    public void PlayShootSound()
    {

        AudioManager.instance.RandomizeSound(laserShootSounds);

    }
    public void PlayCoinSound()
    {

        AudioManager.instance.RandomizeSound(coinSounds);

    }

    public void PlayDefeatSound()
    {

        AudioManager.instance.PlaySingleSound(defeatSound);

    }
    public void PlaySelectSound()
    {

        AudioManager.instance.PlaySingleSound(selectSound);

    }

    public void PlayGameMusic()
    {

        AudioManager.instance.PlayMusic(gameMusic);

    }
    public void PlayMenuMusic()
    {

        AudioManager.instance.PlayMusic(menuMusic);

    }
}