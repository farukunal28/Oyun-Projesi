using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f; 

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (spawnPoints.Length == 0 || objectToSpawn == null)
            return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(objectToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);

  
    }
}


