using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
   
    PlayerController player;

    private void Awake()
    {
       
        player = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if(player._inputValue._Movement.x != 0) {
            triggerAnimationSound();
        }
        else
        {
            StopSound();
        }
    }

    public void triggerAnimationSound()
    {
        FindObjectOfType<SoundManager>().Play("Pivot");
       
    }
    public void StopSound()
    {
        FindObjectOfType<SoundManager>().Stop("Pivot");
      
    }
}
