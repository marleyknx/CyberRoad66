using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{

   [SerializeField] CinemachineVirtualCamera vrCam;
    float currentFov = 30,maxFov = 60;
    public float increaseFov = 10f;



    public void OnEnable()
    {
      //  PlayerEvent.OnDash += IncreaseFov;
    }


    private void OnDisable()
    {
        PlayerEvent.OnDash += DecreaseFov;
    }
    // Start is called before the first frame update
    void Start()
    {
        vrCam = GetComponent<CinemachineVirtualCamera>();
        vrCam.m_Lens.FieldOfView = Mathf.Clamp(vrCam.m_Lens.FieldOfView, currentFov, maxFov);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // public void IncreaseFov() => StartCoroutine(ChangeFov(increaseFov)) ;

    public void DecreaseFov() => vrCam.m_Lens.FieldOfView -= increaseFov * Time.deltaTime;

  
}
