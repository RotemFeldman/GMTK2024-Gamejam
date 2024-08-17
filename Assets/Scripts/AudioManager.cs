using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Components")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource weaponSfx;

    [Header("Music Files")]
    [SerializeField] private AudioClip mainTheme;

    [Header("SFX Files")] 
    [SerializeField] public AudioClip[] Shoot;

    [SerializeField] public AudioClip[] jump;

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


    public void PlayMusic(AudioClip track )
    {
        music.clip = track;
        music.Play();
    }

    public void PlayOneshot(AudioClip sound)
    {
        sfx.clip = sound;
        sfx.Play();
    }

    private int shootArrLocation = 0;
    public void PlayShootSound()
    {
        var sound = Shoot[shootArrLocation];
        weaponSfx.clip = sound;
        weaponSfx.Play();

        shootArrLocation++;
        if (shootArrLocation > Shoot.Length)
            shootArrLocation = 0;
    }

    public void PlayRandomFromArray(AudioClip[] audioClips)
    {
        int rnd = Random.Range(0, audioClips.Length);
        sfx.clip = audioClips[rnd];
        sfx.Play();
    }
}
