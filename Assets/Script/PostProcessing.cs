using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{

   [SerializeField] PostProcessProfile _accelPostProcess;

    PostProcessVolume _postProcessVolume;
    PostProcessProfile _defaultPostProcess;




    private void Awake()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();

        _defaultPostProcess =  _postProcessVolume.profile;
    }



    private void OnEnable()
    {
        PlayerEvent.OnDash += GetAccelPostProcess;
        PlayerEvent.OnStopDash += GetDefaultPostProcess;
    }

    private void OnDisable()
    {
        PlayerEvent.OnDash -= GetAccelPostProcess;
        PlayerEvent.OnStopDash -= GetDefaultPostProcess;
    }






    public void GetAccelPostProcess()
    {
        _postProcessVolume.profile = _accelPostProcess;
    }

    public void GetDefaultPostProcess()
    {

        _postProcessVolume.profile = _defaultPostProcess;
    }


}
