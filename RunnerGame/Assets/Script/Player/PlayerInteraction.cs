using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Player
{

public class PlayerInteraction : MonoBehaviour
{

      [SerializeField]  LayerMask AiMask;
        [SerializeField] Vector3 leftDirection,rightDirection;
        [SerializeField] Transform _leftSide,_rightSide;


        RaycastHit hitInfo;
        public float distanceToHit;
        public ProgressBar progressBar;

        public bool canAddBoost;
        float boostTimer = 0f;
        public float boostCooldown = .5f;

        TextGameFeel _textGameFeel;



        private void Awake()
        {
            _textGameFeel = FindObjectOfType<TextGameFeel>();
        }



        // Update is called once per frame
        void Update()
    {

            if (!canAddBoost)
            {
                boostTimer += Time.deltaTime;
                if (boostTimer >= boostCooldown)
                {
                    canAddBoost = true;
                    boostTimer = 0f;
                }
            }

            Debug.DrawRay(transform.position, leftDirection * hitInfo.distance, Color.red);
            Debug.DrawRay(transform.position, rightDirection * hitInfo.distance, Color.red);
            if (canAddBoost)
            {
                /* if (RightRaycast() || LeftRaycast())
                 {
                     distanceToHit = Vector3.Distance(hitInfo.collider.transform.position, transform.position);
                     //  Debug.Log(hitInfo.collider.name);





                     if (hitInfo.collider.CompareTag("Car"))
                     {
                         RestoringBar();

                     }

                 }*/
                if (RightRaycast())
                {

                    distanceToHit = Vector3.Distance(hitInfo.collider.transform.position, transform.position);

                    
                    if (hitInfo.collider.CompareTag("Car"))
                    {
                        RestoringBar();

                    }
                }
                if (LeftRaycast())
                {

                    distanceToHit = Vector3.Distance(hitInfo.collider.transform.position, transform.position);


                    if (hitInfo.collider.CompareTag("Car"))
                    {
                        RestoringBar();

                    }
                }
            }

    }

    public void RestoringBar()
        {
            if (distanceToHit > 2f && distanceToHit < 2.5f) progressBar.RestoringBar(2);
            if (distanceToHit < 1.5f) progressBar.RestoringBar(15);
            else if (distanceToHit < 2.5f && distanceToHit > 1.5f) progressBar.RestoringBar(5);
            progressBar.UpdateHealthUI();
            canAddBoost = false;

        }

        public   bool RightRaycast() => Physics.Raycast(transform.position ,leftDirection ,out hitInfo,1000,AiMask);
     public bool LeftRaycast() => Physics.Raycast(transform.position,   rightDirection,out hitInfo,1000,AiMask);


       
    }
}
