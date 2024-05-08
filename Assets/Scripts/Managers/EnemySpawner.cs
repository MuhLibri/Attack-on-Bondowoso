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
    private GameObject[] enemySpawned;

    private float timer;
    private bool spawnStarted = false;

    void Start()
    {
        enemySpawned = new GameObject[maxEnemyCount];
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

    public void ResetSpawn() {
        spawnStarted = false;
        DestroySpawned();
        spawned = 0;
    }

    public void RestartSpawn() {
        DestroySpawned();
        spawned = 0;
    }

    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create a new enemy
        GameObject newEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        // Add new enemy to enemySpawned
        enemySpawned[spawned] = newEnemy;
    }

    void DestroySpawned() {
        for (int i = 0; i < maxEnemyCount; i++) {
            Destroy(enemySpawned[i]);
        }
    }
}
