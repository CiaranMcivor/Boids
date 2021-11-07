using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boidPrefab;                                         // boid object reference
    [SerializeField] private int numOfBoids;                                                //Number of boids to spawn, would suggest a cap at 300 - 400, performance may dip significantly based on hardware.
    [SerializeField] private float spawnRadius;                                             // Radius of sphere to spawn inside
    // Start is called before the first frame update
    void Start()
    {
        spawnBoids();
    }

    void spawnBoids()
    {
        for(int i = 0; i < numOfBoids; i++)                                                 
        {
            Vector3 randomPos = this.transform.position + Random.insideUnitSphere * spawnRadius;        // Find a random point inside sphere of  spawnRadius.
            Instantiate(boidPrefab, randomPos, boidPrefab.transform.rotation);                          // Spawn at that position
        }
    }

}
