using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Objects in Main Menu
    public GameObject mainMenu;
    public GameObject loadSaveMenu;
    public GameObject statisticsMenu;
    public GameObject settingsMenu;

    // Objects in Load Save Menu
    public TextMeshProUGUI saveOneText;
    public TextMeshProUGUI saveTwoText;
    public TextMeshProUGUI saveThreeText;

    // Objects in Statistics Menu
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI playtimeText;

    // Objects in Settings Menu
    public TextMeshProUGUI playerNameText;
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;
    public TextMeshProUGUI difficultyText;
    public TMP_Dropdown difficultyDropdown;

    [SerializeField]
    private string fileFormat = "json";
    private string folderPath;

    // Methods on scene initialization
    public void Start()
    {
        folderPath = Application.persistentDataPath;
        UpdateLoadSave();
        UpdateStatistics();
        UpdateSettings();
    }

    // Methods for Main Menu
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void ShowSaveStates()
    {
        loadSaveMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ShowStatistics()
    {
        statisticsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        loadSaveMenu.SetActive(false);
        statisticsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // Methods for Load Save Menu
    public void UpdateLoadSave()
    {
        try
        {
            string[] filePathList = Directory.GetFiles(folderPath, $"*.{fileFormat}");
            SaveData[] saveDatas = new SaveData[filePathList.Length];
            for (int i = 0; i < filePathList.Length; i++)
            {
                saveDatas[i] = LoadSaveData(filePathList[i]);
                if (saveDatas[i].name != null)
                {
                    if (saveDatas[i].name == "Save1")
                    {
                        saveOneText.text = saveDatas[0].name + saveDatas[0].lastSaved;
                    }
                    else if (saveDatas[i].name == "Save2")
                    {
                        saveTwoText.text = saveDatas[1].name + saveDatas[1].lastSaved;
                    }
                    else if (saveDatas[i].name == "Save3")
                    {
                        saveThreeText.text = saveDatas[2].name + saveDatas[2].lastSaved;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("An error occurred: " + e.Message);
        }
    }
    public SaveData LoadSaveData(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded from: " + filePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found at: " + filePath);
            return null;
        }
    }

    // Methods for Statistics Menu
    public void UpdateStatistics()
    {
        string filePath = $"{folderPath}/statistics.{fileFormat}";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StatisticsData statisticsData = JsonUtility.FromJson<StatisticsData>(json);

            int shotsFired = statisticsData.shotsFired;
            int shotsHit = statisticsData.shotsHit;
            float distance = statisticsData.distance;
            float playtime = statisticsData.playtime;

            float accuracy = (shotsFired > 0) ? ((float)shotsHit / shotsFired) * 100 : 0f;
            accuracyText.text = "Accuracy: " + accuracy.ToString("F1") + "%";
            distanceText.text = "Distance: " + distance.ToString("F1") + " kilometer";
            playtimeText.text = "Playtime: " + FormatTime(playtime);
        }
    }
    public string FormatTime(float timeInSeconds)
    {
        // Convert time from seconds to hours, minutes, and seconds
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Methods for Settings Menu
    public void UpdateSettings()
    {
        playerNameText.text = "Player: Guest";
        volumeText.text = "Volume: 100";
        difficultyText.text = "Difficulty: Easy";
    }
    public void ChangePlayerName()
    {
        playerNameText.text = "Player: " + playerNameInput.text;
    }
    public void ChangeVolume()
    {
        volumeText.text = "Volume: " + ((int)(volumeSlider.value * 100)).ToString();
        AudioListener.volume = volumeSlider.value;
    }
    public void ChangeDifficulty()
    {
        int difficultyValue = difficultyDropdown.value;
        string difficultyString;
        switch (difficultyValue)
        {
            case 0:
                difficultyString = "Easy";
                break;
            case 1:
                difficultyString = "Medium";
                break;
            case 2:
                difficultyString = "Hard";
                break;
            default:
                difficultyString = "Unknown";
                break;
        }
        difficultyText.text = "Difficulty: " + difficultyString;
    }
}
