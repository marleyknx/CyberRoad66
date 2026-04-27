using Cinemachine;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinemachineControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vrCam;
    float currentFov = 40, maxFov = 45;
    public float increaseFov = 5f;
    PlayerController player;
  public   bool StopFov = true;


   
    void Awake()
    {
        vrCam = GetComponent<CinemachineVirtualCamera>();
       vrCam.m_Lens.FieldOfView = currentFov;
         player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        vrCam.m_Lens.FieldOfView = Mathf.Clamp(vrCam.m_Lens.FieldOfView, currentFov, maxFov);


        if (StopFov) DecreaseFov();
        else IncreaseFov();
    }


    public void TriggerFov(bool trigger) =>  StopFov= trigger;
    

    public void IncreaseFov()
    {
       
        vrCam.m_Lens.FieldOfView += increaseFov * Time.deltaTime;
    }
    public void DecreaseFov()
    {
       
        vrCam.m_Lens.FieldOfView -= increaseFov * Time.deltaTime;
    }
    


}
