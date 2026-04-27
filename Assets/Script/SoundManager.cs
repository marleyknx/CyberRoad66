using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string _name;
        public AudioClip _clip;
        [Range(0f, 3f)] 
        public float _volume;
        [Range(.1f, 3f)] 
        public float _pitch;
        public bool _loop;
        public bool playOnAwake;
     
       [HideInInspector] public AudioSource _source;
    }

    public Sound[] _sounds;
   

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
      //  DontDestroyOnLoad(this);
        foreach (var s in _sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;
            s._source.volume = s._volume;
            s._source.pitch = s._pitch;
            s._source.loop = s._loop;
            s._source.playOnAwake = s.playOnAwake;
            
        }
    }

    private void Update()
    {
        PauseAllSound(GameManager.Instance.isPausing);
       
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
     Sound s =   Array.Find(_sounds, sound => sound._name == name);
        s._source?.Play();
    }


    public void PlayAccel()
    {
        Sound s = _sounds[0];
        s._source?.Play();
       
       
    }
    public void playGameOverSound()
    {
       
        StartCoroutine(StopGameOverSound());
    }
    IEnumerator StopGameOverSound()
    {
        Sound s = _sounds[_sounds.Length - 1];
        s._source?.Play();
        yield return new WaitForSecondsRealtime(3);
        s._source?.Stop();
       
    }



    public void PauseAllSound(bool isPausing)
    {
        if (isPausing)
        {
            foreach (var s in _sounds)
            {
                s._source?.Pause();
            }
        }
        else if(!isPausing)
        {
            foreach (var s in _sounds)
            {
                s._source?.UnPause();
            }
        }
        

    }
    public void StopAccel()
    {
        float defaultPitch = 0.5f;
        Sound s = _sounds[0];
        s._pitch =defaultPitch;
        s._source?.Stop();
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);
        s._source?.Stop();
    }
}


