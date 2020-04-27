using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    AudioClip[] audioManager;
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audioManager = AudioManager.instance.audioClips;
        
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            audio.clip = audioManager[1];
            audio.Play();
        }
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            audio.clip = audioManager[0];
            audio.Play();
        }
        if (SceneManager.GetActiveScene().name == "GamaScene")
        {
            audio.clip = audioManager[2];
            audio.Play();
        }
    }
}
