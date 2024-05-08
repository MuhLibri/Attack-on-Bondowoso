using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questTitle;
    public string questObjective;
    public int reward;
    public GameObject enemySpawner;
    private EnemySpawner[] enemySpawners;

    [SerializeField]
    private int targetKill;
    private int killed = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawners = enemySpawner.GetComponents<EnemySpawner>();
        targetKill = 0;
        for (int i = 0; i < enemySpawners.Length; i++) {
            targetKill += enemySpawners[i].maxEnemyCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // UnityEngine.Debug.Log("Target: " + targetKill);
    }

    void StartQuest() {
        // TO DO Implement
    }

    bool IsFinished() {
        // TO DO Implement
        return targetKill == killed;
    }

    void FinishQuest() {
        // TO DO Implement
    }

    public void AddKilled() {
        UnityEngine.Debug.Log("Add killed");
        killed++;
    }

    public int GetTargetKill() {
        return targetKill;
    }

    public int GetKilled() {
        return killed;
    }
}
