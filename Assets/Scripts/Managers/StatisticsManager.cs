using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public int shotsFired;
    public int shotsHit;
    public float distanceTraveled;
    public float startTime;
    public int goldEarned;
    public int killCount;
    public int saveCount;
    public string fileFormat = "json";
    public string folderPath;

    private static StatisticsManager instance;
    public static StatisticsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StatisticsManager>();
            }
            return instance;
        }
    }

    public void Start()
    {
        folderPath = Application.persistentDataPath;
        shotsFired = 0;
        shotsHit = 0;
        distanceTraveled = 0f;
        goldEarned = 0;
        killCount = 0;
        saveCount = 0;
        startTime = Time.time;
    }

    public void OnApplicationQuit()
    {
        SaveStatistics();
    }

    public void ShotFired()
    {
        shotsFired++;
        Debug.Log("Shots fired this session: " + shotsFired);
    }

    public void ShotHit()
    {
        shotsHit++;
        Debug.Log("Shots Hit this session: " + shotsHit);
    }

    public void UpdateDistanceTraveled(float distance)
    {
        distanceTraveled += distance / 1000;
        Debug.Log("Distance Traveled this session: " +  distanceTraveled);
    }

    public void UpdateGold(int amount)
    {
        goldEarned += amount;
        Debug.Log("Gold Earned this session: " +  goldEarned);
    }

    public void KillCount()
    {
        killCount++;
        Debug.Log("Kill Count this session: " + killCount);
    }

    public void SaveCount()
    {
        saveCount++;
        Debug.Log("Save Count this session: " +  saveCount);
    }

    public void SaveStatistics()
    {
        string filePath = $"{folderPath}/Statistics.{fileFormat}";
        if (File.Exists(filePath))
        {
            string oldJson = File.ReadAllText(filePath);
            StatisticsData oldStatisticsData = JsonUtility.FromJson<StatisticsData>(oldJson);

            int oldShotsFired = oldStatisticsData.shotsFired;
            int oldShotsHit = oldStatisticsData.shotsHit;
            float oldDistance = oldStatisticsData.distance;
            float oldPlaytime = oldStatisticsData.playtime;
            int oldGold = oldStatisticsData.gold;
            int oldKill = oldStatisticsData.kill;
            int oldSave = oldStatisticsData.save;

            int newShotsFired = oldShotsFired + shotsFired;
            int newShotsHit = oldShotsHit + shotsHit;
            float newDistance = oldDistance + distanceTraveled;
            float newPlaytime = oldPlaytime + (Time.time - startTime);
            int newGold = oldGold + goldEarned;
            int newKill = oldKill + killCount;
            int newSave = oldSave + saveCount;

            StatisticsData newStatisticsData = new StatisticsData(newShotsFired, newShotsHit, newDistance, newPlaytime, newGold, newKill, newSave);
            string newJson = JsonUtility.ToJson(newStatisticsData);
            File.WriteAllText(filePath, newJson);
        } 
        else
        {
            StatisticsData statisticsData = new StatisticsData(shotsFired, shotsHit, distanceTraveled, Time.time - startTime, goldEarned, killCount, saveCount);
            string json = JsonUtility.ToJson(statisticsData);
            File.WriteAllText(filePath, json);
        }
    }

    public StatisticsData GetStatistics()
    {
        StatisticsData statisticsData = new StatisticsData(shotsFired, shotsHit, distanceTraveled, Time.time - startTime, goldEarned, killCount, saveCount);
        return statisticsData;
    }
}
