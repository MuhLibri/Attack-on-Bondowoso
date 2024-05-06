using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private float timer;
    // private int spawned = 0;

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = spawnTime;
        }
    }

    void Spawn()
    {
        // If the player has no health left...
        // if (playerHealth.currentHealth <= 0f)
        // {
        //     // ... exit the function.
        //     return;
        // }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}