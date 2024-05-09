using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    // Variables to track statistics
    int shotsFired;
    int shotsHit;
    float distanceTraveled;
    float startTime;
    int goldEarned;
    int killCount;
    string fileFormat = "json";
    string folderPath;

    void Start()
    {
        // Load saved statistics data or initialize if it doesn't exist
        folderPath = Application.persistentDataPath;
        shotsFired = 0;
        shotsHit = 0;
        distanceTraveled = 0f;
        goldEarned = 0;
        killCount = 0;
        startTime = Time.time;
    }

    void OnApplicationQuit()
    {
        // Save statistics data when the game quits
        SaveStatistics();
    }

    public void ShotFired(int bullets)
    {
        shotsFired += bullets;
        Debug.Log("Shots fired this session: " + shotsFired);
    }

    public void ShotHit()
    {
        // Increment shots hit
        shotsHit++;
    }

    public void UpdateDistanceTraveled(float distance)
    {
        // Calculate distance traveled
        distanceTraveled += distance;
        Debug.Log("Distance Traveled this session: " +  distanceTraveled);
    }

    public void KillCount()
    {
        killCount++;
        Debug.Log("Kill Count this session: " + killCount);
    }

    public void UpdateGold(int amount)
    {
        goldEarned += amount;
        Debug.Log("Gold Earned this session: " +  goldEarned);
    }

    void SaveStatistics()
    {
        // Save statistics data
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

            int newShotsFired = oldShotsFired + shotsFired;
            int newShotsHit = oldShotsHit + shotsHit;
            float newDistance = oldDistance + distanceTraveled;
            float newPlaytime = oldPlaytime + (Time.time - startTime);
            int newGold = oldGold + goldEarned;
            int newKill = oldKill + killCount;

            StatisticsData newStatisticsData = new StatisticsData(newShotsFired, newShotsHit, newDistance, newPlaytime, newGold, newKill);
            string newJson = JsonUtility.ToJson(newStatisticsData);
            File.WriteAllText(filePath, newJson);
        } 
        else
        {
            StatisticsData statisticsData = new StatisticsData(shotsFired, shotsHit, distanceTraveled, Time.time - startTime, goldEarned, killCount);
            string json = JsonUtility.ToJson(statisticsData);
            File.WriteAllText(filePath, json);
        }
    }
}
