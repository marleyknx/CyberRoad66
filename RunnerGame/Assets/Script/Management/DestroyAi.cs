using Management;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyAi : MonoBehaviour
{


    float score = 0;
    [SerializeField] TMP_Text txtScore;
    public Scrolling _scroll;
    bool canCount = true;

    public bool CanCount { get => canCount; set => canCount = value; }

    private void Awake()
    {
        txtScore.text = score.ToString("Score : " + "00000000"); 
    }

    private void Update()
    {
        if(canCount)
        score += (Time.deltaTime * 100);
        _scroll.OffsetSpeed = Mathf.Clamp(_scroll.OffsetSpeed, 0,7);
        
        txtScore.text = score.ToString ("Score : " + "00000000");
    }

    public void AddScore(int addscore)
    {
        score += addscore;
    }
    private void OnCollisionEnter(Collision collision)
    {

        _scroll.OffsetSpeed += .5f;
        Destroy(collision.transform.parent.gameObject);
        //print(collision.gameObject.name);
    }
}
