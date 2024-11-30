using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource soundEffectSource;
    public AudioSource musicSource;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    //Ajoutez votre clip audio ici
    //Pour le jouer, utilisez AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.nomDuClip);
    public AudioClip soundEffect1;
    public AudioClip soundEffect2;
    public AudioClip soundEffect3;

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

    private void Start()
    {
        PlayMusic(1);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }

    public void PlayMusic(int musicIndex)
    {
        AudioClip selectedMusic = null;

        switch (musicIndex)
        {
            case 1:
                selectedMusic = musicClip1;
                break;
            case 2:
                selectedMusic = musicClip2;
                break;
        }

        if (selectedMusic != null && musicSource.clip != selectedMusic)
        {
            musicSource.clip = selectedMusic;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}