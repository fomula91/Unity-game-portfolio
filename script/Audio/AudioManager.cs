using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public static AudioManager instance = null;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        audioClips = Resources.LoadAll<AudioClip>("BGM/");
        DontDestroyOnLoad(this.gameObject);
    }

    void audioclip()
    {
        for(int i = 0; i < audioClips.Length; i++)
        {
            audioClips = Resources.LoadAll<AudioClip>("BGM/");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
