using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Management
{
    public class Scrolling : MonoBehaviour
{


        [Range(.0f, 10f)]
        [SerializeField] float _offsetSpeed = 1;
        Renderer rd;
       public float _offset;

        public float OffsetSpeed { get => _offsetSpeed; set => _offsetSpeed = value; }

        private void Awake()
        {
            rd = GetComponent<Renderer>();  
        }

        private void OnEnable()
        {
            PlayerEvent.OnDash += Accelerate;
        }

        private void OnDisable()
        {

            PlayerEvent.OnDash -= Accelerate;
        }

        void Start()
    {
        
    }


    void Accelerate()
        {
            OffsetSpeed += OffsetSpeed * 2 * Time.deltaTime;
        }
    void Deccelerate()
        {
            OffsetSpeed -= OffsetSpeed * 2 * Time.deltaTime;
        }
   
    void Update()
    {
        _offset += (Time.deltaTime * OffsetSpeed);
            rd.material.SetTextureOffset("_MainTex",new Vector2(0,-_offset));
    }
}
}
