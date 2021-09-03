using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] dronePreFabs;
    private float spawnRange = 5.0f;

    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDrone", startDelay, spawnInterval);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnDrone()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange),
            0, Random.Range(-spawnRange, spawnRange));
        int animalIndex = Random.Range(0, dronePreFabs.Length);
        Instantiate(dronePreFabs[animalIndex], spawnPos,
            dronePreFabs[animalIndex].transform.rotation); 
    }
}
