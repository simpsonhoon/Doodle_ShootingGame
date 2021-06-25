using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class AudioManagerScript : MonoBehaviour
{
    List<AudioSource> audioSources; 
    public List<AudioClip> musicClips;
    public List<AudioClip> soundClips;
    public static AudioManagerScript instance;  //singleton
   

    void Awake()
    {
         if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSources = new List<AudioSource>();
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.1f;
        audioSources.Add(audioSource);

        audioSources.Add(gameObject.AddComponent<AudioSource>());
        audioSources.Add(gameObject.AddComponent<AudioSource>());
        audioSources.Add(gameObject.AddComponent<AudioSource>());
    }

    public void PlayMusic(Music id)
    {
        audioSources[0].Stop();
        audioSources[0].clip = musicClips[(int)id];
        audioSources[0].Play();
    }

    public void PlaySound(Sound id)
    {
        switch (id)
        {
            case Sound.PlayerShot:
                audioSources[1].Stop();
                audioSources[1].clip = soundClips[(int)id];
                audioSources[1].Play();
                break;
            case Sound.Explosion:
                audioSources[2].Stop();
                audioSources[2].clip = soundClips[(int)id];
                audioSources[2].Play();
                break;
            case Sound.Coin:
                audioSources[3].Stop();
                audioSources[3].clip = soundClips[(int)id];
                audioSources[3].Play();
                break;
        }
        
    }

}
