using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questTitle;
    public string questObjective;
    public int reward = 5000;
    public GameObject enemySpawner;
    private EnemySpawner[] enemySpawners;

    private int targetKill;
    private int killed = 0;
    StatisticsManager statisticsManager;

    // Start is called before the first frame update
    void Awake()
    {
        enemySpawners = enemySpawner.GetComponents<EnemySpawner>();
    }

    void Start() {
        targetKill = 0;
        for (int i = 0; i < enemySpawners.Length; i++) {
            targetKill += enemySpawners[i].maxEnemyCount;
        }
        statisticsManager = GetComponent<StatisticsManager>();
    }

    public void StartQuest() {
        for (int i = 0; i < enemySpawners.Length; i++) {
            enemySpawners[i].StartSpawn();
        }
    }

    public bool IsFinished() {
        return (targetKill == killed) && (targetKill != 0);
    }

    public void FinishQuest() {
        PlayerGold.GiveGold(reward);
        statisticsManager.UpdateGold(reward);
    }

    public void ResetQuest() {
        killed = 0;
        for (int i = 0; i < enemySpawners.Length; i++) {
            enemySpawners[i].ResetSpawn();
        }
    }

    public void AddKilled() {
        killed++;
        statisticsManager.KillCount();
    }

    public int GetTargetKill() {
        return targetKill;
    }

    public int GetKilled() {
        return killed;
    }
}
