using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundTest : MonoBehaviour
{
  
   public AudioSource source;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && source.isPlaying != true)
        {
            source.Play();
            Debug.Log("je joue un son");
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            source.Stop();
            Debug.Log("je joue plus le son");
        }
    }
}
