using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement Instance;  
    private AudioSource audioSource;   

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
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; 
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
    }

    public void PlayMusic(RoomMusicSO roomMusicClip)
    {
        if (audioSource.clip != roomMusicClip.roomMusicClip)
        {
            audioSource.clip = roomMusicClip.roomMusicClip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}