using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    // References to UI Text elements to display statistics
    public Text accuracyText;
    public Text distanceText;
    public Text playtimeText;

    // Variables to track statistics
    private int totalShotsFired;
    private int shotsHit;
    private float totalDistanceTraveled;
    private float startTime;

    private void Start()
    {
        // Load saved statistics data or initialize if it doesn't exist
        totalShotsFired = PlayerPrefs.GetInt("TotalShotsFired", 0);
        shotsHit = PlayerPrefs.GetInt("ShotsHit", 0);
        totalDistanceTraveled = PlayerPrefs.GetFloat("TotalDistanceTraveled", 0f);
        startTime = PlayerPrefs.GetFloat("StartTime", Time.time);
    }

    private void OnApplicationQuit()
    {
        // Save statistics data when the game quits
        SaveStatistics();
    }

    private void Update()
    {
        // Update playtime
        float playtime = Time.time - startTime;
        playtimeText.text = "Playtime: " + FormatTime(playtime);

        // Update UI with statistics
        UpdateAccuracy();
        UpdateDistanceTraveled();
    }

    public void ShotFired()
    {
        // Increment total shots fired
        totalShotsFired++;
    }

    public void ShotHit()
    {
        // Increment shots hit
        shotsHit++;
    }

    private void UpdateAccuracy()
    {
        // Calculate accuracy percentage
        float accuracy = (totalShotsFired > 0) ? ((float)shotsHit / totalShotsFired) * 100 : 0f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("F1") + "%";
    }

    private void UpdateDistanceTraveled()
    {
        // Calculate distance traveled
        float distanceThisFrame = Vector3.Distance(transform.position, transform.position); // Replace transform.position with actual player position
        totalDistanceTraveled += distanceThisFrame;
        distanceText.text = "Distance Traveled: " + totalDistanceTraveled.ToString("F1") + " units";
    }

    private void SaveStatistics()
    {
        // Save statistics data
        PlayerPrefs.SetInt("TotalShotsFired", totalShotsFired);
        PlayerPrefs.SetInt("ShotsHit", shotsHit);
        PlayerPrefs.SetFloat("TotalDistanceTraveled", totalDistanceTraveled);
        PlayerPrefs.SetFloat("StartTime", startTime);
        PlayerPrefs.Save();
    }

    private string FormatTime(float timeInSeconds)
    {
        // Convert time from seconds to hours, minutes, and seconds
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
