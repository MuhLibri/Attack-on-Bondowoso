using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public int maxEnemyCount = 5;
    private int spawned = 0;
    private List<GameObject> enemies = new List<GameObject>();
    public Transform[] spawnPoints;

    private float timer;

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !StopSpawn())
        {
            Spawn();
            spawned++;
            timer = spawnTime;
        }
        // Use this to get enemy remaining from enemy list
        // Debug.Log("Enemy remaining: " + enemies.Count);
    }

    bool StopSpawn()
    {
        return spawned == maxEnemyCount;
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
        GameObject newEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        // Add enemy to enemy list
        enemies.Add(newEnemy);
    }
}
