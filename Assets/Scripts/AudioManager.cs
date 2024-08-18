using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource, _sfxSource;
    [SerializeField] private AudioClip _menuMusic, _gameMusic;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if(Instance != null)
                Destroy(gameObject);
        }
    }
   
    
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("Audio Clip Is Null");
            return;
        }


        _sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, Transform spawn, float volume)
    {
        var source = Instantiate(_sfxSource, spawn.position, Quaternion.identity);

        //source.clip = clip;
        source.transform.position = spawn.position;
        source.volume = volume;
        source.PlayOneShot(clip);

        float length = clip.length;
        Destroy(source.gameObject, length);
    }


   
}
