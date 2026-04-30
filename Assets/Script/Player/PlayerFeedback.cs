using Cinemachine;
using Player;
using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerFeedback : MonoBehaviour
{
    Collider _coll;
    public ParticleSystem[] _particles;
    public ParticleSystem[] _Sparkparticles;
    private SoundManager _SoundManager;

    [SerializeField] CinemachineImpulseSource _impulse;
    [SerializeField] CinemachineImpulseSource _Accelimpulse;

    public float _AccelValue;
   public Explosion _Explosion;

    private void Awake()
    {
        _coll = GetComponent<Collider>();
        _SoundManager = FindFirstObjectByType<SoundManager>();
    }

    private void OnEnable()
    {
        PlayerEvent.OnDash += ActiveParticle;
        PlayerEvent.OnDash += DisableColl;

        PlayerEvent.OnStopDash += DisableParticle;
        PlayerEvent.OnStopDash += ActiveColl;


    }

    private void OnDisable()
    {
        PlayerEvent.OnDash -= ActiveParticle;
        PlayerEvent.OnDash -= DisableColl;

        PlayerEvent.OnStopDash -= DisableParticle;
        PlayerEvent.OnStopDash -= ActiveColl;

    }

     public void PlaySparkParticle(float amount)
    {
        switch (amount)
        {
            case > 0:
                _Sparkparticles[0].Play(); _Sparkparticles[1].Stop();
                break;
            case < 0:
                _Sparkparticles[1].Play(); _Sparkparticles[0].Stop();
                break;

            case 0:
                _Sparkparticles[0].Stop(); _Sparkparticles[1].Stop();
                break;
        }

        if (amount > 0) {  }
        else if (amount < 0) {  }
        if (amount == 0) {  }

    }

    public void PlayInvincibility()
    {
        _coll.isTrigger = true;
        StartCoroutine(StopInvicibility());
    }

    IEnumerator StopInvicibility()
    {
        yield return new WaitForSeconds(1);
        _coll.isTrigger = false;

    }
    public void DashScreenShake() => _Accelimpulse.GenerateImpulse();
    public void DisableColl()
    {
        DashScreenShake();
        // _audioSource.Stop();



        var source = _SoundManager._sounds[0]._source;

        if (source.isPlaying != true)
            _SoundManager.PlayAccel();
        _SoundManager.Stop("Motor");
        _coll.isTrigger = true;

    }
    public void ActiveColl()
    {
        CinemachineImpulseManager.Instance.Clear();
        _SoundManager.StopAccel();

        _SoundManager.Play("Motor");

        _coll.isTrigger = false;


    }


   

    public void ActiveParticle()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            if (_AccelValue != 0)
            {

                _particles[i].loop = true;

                // PlaySound(_diveClip);
                _particles[i].Play();

            }

        }
    }

    public void DisableParticle()
    {


        for (int i = 0; i < _particles.Length; i++)
        {

            _particles[i].loop = false;
            // PlaySound(_motorClip);
            _particles[i].Stop();
        }
    }

}
