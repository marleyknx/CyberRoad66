using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{

    public float Health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    public float boostConsumptionRate = 20f;

    public float recoveryRate = 10f;

    private void OnEnable()
    {
        PlayerEvent.OnDash += ActiveDash;
    }
    private void OnDisable()
    {
        PlayerEvent.OnDash -= ActiveDash;
    }


    private void Start()
    {
        Health = Mathf.Clamp(Health, 0, maxHealth);

    }
    // Update is called once per frame
    void Update()
    {
       
            UpdateHealthUI();
      

    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = Health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float PercentComplete = lerpTimer / chipSpeed;
            PercentComplete = PercentComplete * PercentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, PercentComplete);
        }
    }

    public void RestoreBar()
    {
        Health += recoveryRate * Time.deltaTime;
        
       
    }
    public void RestoringBar(float addHealth)
    {
        Health += addHealth;
        
        lerpTimer = 0;
    }
    public void ActiveDash()
    {
       


        if (Health > 0)
        {

             Health -= boostConsumptionRate * Time.deltaTime;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            lerpTimer = 0;

        }


    }
}
