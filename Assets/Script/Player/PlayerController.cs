using Cinemachine;
using Management;
using PlayerInputHandler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
       
        InputHandler _inputHandler;
       public InputValue _inputValue { get;private set; }


        private PlayerInteraction playerInteraction;
        private Movement _Movement;

        [HideInInspector] public GameObject _road, _grass;

       
        public GameManager GameManager;
       

        public cinemachineControl _cinema;
        Animator _anim;
         
      PlayerFeedback _feedback;
       
       

      


       


        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _road = GameObject.Find("Road").gameObject;
            _grass = GameObject.Find("Grass").gameObject;
            playerInteraction = GetComponent<PlayerInteraction>();
            _feedback = GetComponent<PlayerFeedback>();
           
           _anim = GetComponentInChildren<Animator>();
            _Movement = GetComponentInChildren<Movement>();

           
        }
      

        void Update()
        {
            _feedback._AccelValue = playerInteraction.progressBar.Value;
            _feedback.PlaySparkParticle(_inputHandler._inputValue._Movement.x);

            _Movement.Move(_inputValue._Movement.x,transform.right);

            // quand j'appuie verifier si ma barre est pas vide active ou desactive
            if (_inputHandler._inputValue._DashDown)
            {

                if (Mathf.Round(playerInteraction.progressBar.Value) <= 0)
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
             if( Mathf.Round(playerInteraction.progressBar.Value ) <= 0 )
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





        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Car"))
            {
               _feedback._Explosion.OnExplosion?.Invoke();
                _feedback.PlayInvincibility();
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



                if (!_feedback._IsInvincible)
                {
                    Debug.Log("Detruis");
                LevelManager level = FindAnyObjectByType<LevelManager>();
                level._IsDead = true;
                GameObject model = transform.GetChild(0).gameObject;
                Destroy(model);
                
                _feedback._Explosion.OnExplosion?.Invoke();
                }
              

            }
           
        }
     
    }
}
