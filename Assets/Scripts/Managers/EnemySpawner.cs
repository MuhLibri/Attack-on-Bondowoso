using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public int maxEnemyCount = 5;
    private int spawned = 0;
    public Transform[] spawnPoints;

    private float timer;
    private bool spawnStarted = false;

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (spawnStarted && timer <= 0f && !StopSpawn())
        {
            Spawn();
            spawned++;
            timer = spawnTime;
        }
    }

    public void StartSpawn() {
        spawnStarted = true;
    }

    bool StopSpawn()
    {
        return spawned == maxEnemyCount;
    }

    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject newEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
