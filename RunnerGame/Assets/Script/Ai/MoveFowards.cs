using Cinemachine;
using Management;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemie
{

    public class MoveFowards : MonoBehaviour
    {
        [SerializeField] float _towardsSpeed =10f;
        PlayerController _player;
        Explosion _explosion;


         Collider _boxColl;
       

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
            _explosion = transform.Find("ExplosionFx").GetComponent<Explosion>();
            _boxColl = GetComponent<BoxCollider>();
        }



        // Update is called once per frame
        void Update()
         {
            transform.Translate(Vector3.forward * _towardsSpeed * Time.deltaTime);
         }



        private void OnCollisionEnter(Collision collision)
        {
            
            if(collision.collider.tag == "DestroyCar")
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // je m'auto detruis si le joueur passe a travers de moi
            if (other.tag == "Player")
            {
                FindObjectOfType<DestroyAi>().AddScore(1000);
                Debug.Log("j'explose !");
                // disabled le boxcoll
                _boxColl.enabled = false;
                // rend le joeur invincible pdnt l'explosion
                _player.PlayInvincibility();
                //permet d'apler que le gameobject concernée
                GameObject go = transform.Find("ExplosionFx").gameObject;
                go.GetComponent<Explosion>().OnExplosion?.Invoke();
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

}