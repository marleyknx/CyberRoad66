using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInputHandler;
using Management;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Cinemachine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
       
        InputHandler _inputHandler;
       public InputValue _inputValue { get;private set; }


        private PlayerInteraction playerInteraction;
        private Movement _Movement;

        [HideInInspector] public GameObject _road, _grass;

        public ParticleSystem[] _particles;
        public ParticleSystem[] _Sparkparticles;
        public GameManager GameManager;
        public SoundManager _SoundManager;

        public cinemachineControl _cinema;
        Animator _anim;
         Collider _coll;
        [SerializeField] CinemachineImpulseSource _impulse;
        [SerializeField] CinemachineImpulseSource _impulseSpeed;
       

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


        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _road = GameObject.Find("Road").gameObject;
            _grass = GameObject.Find("Grass").gameObject;
            playerInteraction = GetComponent<PlayerInteraction>();
            _coll = GetComponentInChildren<Collider>();
            _SoundManager = FindFirstObjectByType<SoundManager>();
           _anim = GetComponentInChildren<Animator>();
           
        }
      

        void Update()
        {




            _Movement.Move(_inputValue._Movement.x,transform.right);

            
            

            if (_inputValue._Movement.x > 0) { _Sparkparticles[0].Play(); _Sparkparticles[1].Stop(); }
            else if (_inputValue._Movement.x < 0) { _Sparkparticles[1].Play(); _Sparkparticles[0].Stop(); }
            if(_inputValue._Movement.x ==0) { _Sparkparticles[0].Stop(); _Sparkparticles[1].Stop(); }

            // quand j'appuie verifier si ma barre est pas vide active ou desactive
            if (_inputHandler._inputValue._DashDown)
            {

                if (Mathf.Round(playerInteraction.progressBar.Health) <= 0)
                {
                    _cinema.TriggerFov(true);

                    PlayerEvent.OnStopDash?.Invoke();
                }
                else 
                {
                   

                    _cinema.TriggerFov(false);
                    PlayerEvent.OnDash?.Invoke();

                }


            }
            // pas appuy�e verrifie si ma barre est vide desactive tout
             if( Mathf.Round(playerInteraction.progressBar.Health ) <= 0 )
            {
                _cinema.TriggerFov(true);
               
                PlayerEvent.OnStopDash?.Invoke();

            }
            // si je lache la touche je desactive tout comme de base
            if (_inputHandler._inputValue._DashUp)
            {
                _cinema.TriggerFov(true);
              //  SoundManager.PlaySound(SoundManager.Sound.playerMotor);
                PlayerEvent.OnStopDash?.Invoke();

            }

                  _inputValue = _inputHandler._inputValue;
           
                playerInteraction.progressBar.RestoreBar();
               if(_anim) _anim.SetFloat("MoveX", _inputValue._Movement.x);
          
        }

        public void DashScreenShake() => _impulse.GenerateImpulse();



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



        public void DisableColl()
        {
            DashScreenShake();
            // _audioSource.Stop();

          

          var source = _SoundManager._sounds[0]._source;
            
            if(source.isPlaying != true)
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
                if(playerInteraction.progressBar.Health != 0)
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

       
      


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Car"))
            {
                print("je canne");
                _road.GetComponent<Scrolling>().enabled = false;
                _grass.GetComponent<Scrolling>().enabled = false;
                GameObject.FindFirstObjectByType<Spawn>().StopSpawn();



                //GameManager.Instance._isDead = true;
                LevelManager level = FindFirstObjectByType<LevelManager>();
                level._IsDead = true;
                GameObject model = transform.GetChild(0).gameObject;
                Destroy(model);
                GameObject go = _impulse.gameObject;
                go.GetComponent<Explosion>().OnExplosion?.Invoke();
              

            }
           
        }
     
    }
}
