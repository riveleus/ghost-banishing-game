using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject batteryPrefab;

    [SerializeField]
    private Transform[] spawnLocations;

    /*[SerializeField]
    private int totalBatteryMinimum = 3;*/

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnLocations.Length/2; i++)
        {
            Instantiate(batteryPrefab, spawnLocations[Random.Range(0, spawnLocations.Length -1)].position, batteryPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
