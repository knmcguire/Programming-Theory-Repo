using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject crazyfliePrefab;
    public GameObject boltPrefab;
    public GameObject batteryPrefab;

    private float spawnRange = 5.0f;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpawnRange();
        InitiateSpawnDronesBatteries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSpawnRange()
    {
        MeshCollider[] floorCollider = GameObject.FindWithTag("Floor").GetComponents<MeshCollider>();
        spawnRange = floorCollider[0].bounds.extents.x;
    }

    void InitiateSpawnDronesBatteries()
    {
        InvokeRepeating("SpawnCrazyflie", startDelay, spawnInterval);
        InvokeRepeating("SpawnBolt", startDelay, 5 * spawnInterval);
        InvokeRepeating("SpawnBattery", startDelay, 5 * spawnInterval);
    }

    void SpawnBattery()
    {
        SpawnPrefab(batteryPrefab);
    }

    void SpawnCrazyflie()
    {
        SpawnPrefab(crazyfliePrefab);
    }

    void SpawnBolt()
    {
        SpawnPrefab(boltPrefab);
    }

    void SpawnPrefab(GameObject newPrefab)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange),
            0, Random.Range(-spawnRange, spawnRange));
        Instantiate(newPrefab, spawnPos,
            newPrefab.transform.rotation); 
    }
}
