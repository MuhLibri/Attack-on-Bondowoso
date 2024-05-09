using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    // Variables to track statistics
    private int shotsFired;
    private int shotsHit;
    private float distanceTraveled;
    private float startTime;

    private void Start()
    {
        // Load saved statistics data or initialize if it doesn't exist
        shotsFired = 0;
        shotsHit = 0;
        distanceTraveled = 0f;
        startTime = Time.time;
    }

    private void OnApplicationQuit()
    {
        // Save statistics data when the game quits
        SaveStatistics();
    }

    private void Update()
    {
        UpdateDistanceTraveled();
    }

    public void ShotFired()
    {
        // Increment total shots fired
        shotsFired++;
    }

    public void ShotHit()
    {
        // Increment shots hit
        shotsHit++;
    }

    private void UpdateDistanceTraveled()
    {
        // Calculate distance traveled
        float distanceThisFrame = Vector3.Distance(transform.position, transform.position); // Replace transform.position with actual player position
        distanceTraveled += distanceThisFrame;
    }

    private void SaveStatistics()
    {
        // Save statistics data
        string filePath = Application.persistentDataPath + "/statistics.json";
        if (File.Exists(filePath))
        {
            string oldJson = File.ReadAllText(filePath);
            StatisticsData oldStatisticsData = JsonUtility.FromJson<StatisticsData>(oldJson);

            int oldShotsFired = oldStatisticsData.shotsFired;
            int oldShotsHit = oldStatisticsData.shotsHit;
            float oldDistance = oldStatisticsData.distance;
            float oldPlaytime = oldStatisticsData.playtime;

            int newShotsFired = oldShotsFired + shotsFired;
            int newShotsHit = oldShotsHit + shotsHit;
            float newDistance = oldDistance + distanceTraveled;
            float newPlaytime = oldPlaytime + (Time.time - startTime);

            StatisticsData newStatisticsData = new StatisticsData(newShotsFired, newShotsHit, newDistance, newPlaytime);
            string newJson = JsonUtility.ToJson(newStatisticsData);
            File.WriteAllText(filePath, newJson);
        } 
        else
        {
            StatisticsData statisticsData = new StatisticsData(shotsFired, shotsHit, distanceTraveled, Time.time - startTime);
            string json = JsonUtility.ToJson(statisticsData);
            File.WriteAllText(filePath, json);
        }
    }
}
