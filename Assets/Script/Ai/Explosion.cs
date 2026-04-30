using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    public  Action OnExplosion;
   [SerializeField] CinemachineImpulseSource _impulse;


    private void Awake()
    {
           

    }

    private void OnEnable()
    {
        OnExplosion += GetScreenShake;
        OnExplosion += PlayExplosion;
    }


    private void OnDisable()
    {
        OnExplosion -= GetScreenShake;
        OnExplosion -= PlayExplosion;
    }
    public void PlayExplosion()
    {
        _particleSystem.gameObject.SetActive(true);
       _particleSystem.Play();
        StartCoroutine(StopExplosion());
        FindObjectOfType<SoundManager>().Play("Explosion");

    }

  

    IEnumerator StopExplosion()
    {
        yield return new WaitForSeconds(1);
        _particleSystem.gameObject.SetActive(false);
    }
    public void GetScreenShake()
    {
        _impulse.GenerateImpulse();
    }
}
