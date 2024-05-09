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
    private string fileFormat = "json";
    private string folderPath;

    private void Start()
    {
        // Load saved statistics data or initialize if it doesn't exist
        folderPath = Application.persistentDataPath;
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

    private void SaveStatistics()
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
