using Enemie;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Management
{

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] _car;
    [SerializeField] float _spawnRate = 2;
    [SerializeField] Transform[] spawnTransform;
    int numbOfCar =0;
    void Start()
    {
        StartCoroutine(SpawnCar());
    }

   IEnumerator SpawnCar()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
                numbOfCar++;
            if(numbOfCar % 5 == 0)
            {
                _spawnRate -= .1f;
               _spawnRate =  Mathf.Clamp(_spawnRate, .5f, 2.5f);

            }
                
            int randoms = Random.Range(0, _car.Length);
            int spawnRandom = Random.Range(0, spawnTransform.Length);
            Instantiate(_car[randoms], spawnTransform[spawnRandom].position,transform.rotation);

        }
    }


        public void StopSpawn()
        {
            StopAllCoroutines();
            GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
            DestroyAi stopScore = FindFirstObjectByType<DestroyAi>();
            stopScore.CanCount = false;
           
            foreach (GameObject car in cars)
            {
                car.GetComponentInParent<MoveFowards>().enabled = false;
            }
        }
}
}
