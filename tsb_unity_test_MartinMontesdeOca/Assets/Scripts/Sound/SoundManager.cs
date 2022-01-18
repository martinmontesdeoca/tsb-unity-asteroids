using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    Dictionary<string, AudioSource> soundDictionary;
    AudioSource audioSource;    
    private GameObject gameObjectAudioSource;
    [SerializeField]
    private AudioClip[] soundsArray;   

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundManager is NUll!!");
            }

            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;       
    }
  
    void Start()
    {        
        CreateAllSounds();
    }

    void CreateAllSounds()
    {
        soundDictionary = new Dictionary<string, AudioSource>();
        foreach (var sound in soundsArray)
        {
            gameObjectAudioSource = new GameObject();
            gameObjectAudioSource.name = sound.name;
            audioSource = gameObjectAudioSource.AddComponent<AudioSource>();
            audioSource.volume = 0.5f;
            audioSource.clip = (sound as AudioClip);
            soundDictionary[sound.name] = audioSource;
        }
    }

    public void PlaySound(string name,bool playOver = false)
    {
        if(!soundDictionary[name].isPlaying || playOver)
            soundDictionary[name].Play();
    }

    public void StopSound(string name)
    {
        soundDictionary[name].Stop();
    }
}
