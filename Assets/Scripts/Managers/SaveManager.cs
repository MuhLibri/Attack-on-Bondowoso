using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SaveManager : MonoBehaviour
{
    public static string fileFormat = "json";
    private static string folderPath;
    public GameObject questBox;
    private QuestManager questManager;
    private static bool isLoaded = false;
    StatisticsManager statisticsManager;

    // Start is called before the first frame update
    void Start()
    {
        folderPath = Application.persistentDataPath;
        questManager = questBox.GetComponent<QuestManager>();
        statisticsManager = GetComponent<StatisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string fileName = "Save1";
        if (isLoaded) {
            isLoaded = false;
            string filePath = $"{folderPath}/{fileName}.{fileFormat}";
            LoadGame(filePath);
        }
        // TO DO make new way to save game
        if (Input.GetKeyDown(KeyCode.P)) {
            SaveData saveData = new SaveData(fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
            SaveData(saveData);
        }
        
        if (Input.GetKeyDown(KeyCode.L)) {
            string filePath = $"{folderPath}/{fileName}.{fileFormat}";
            LoadGame(filePath);
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            SaveData[] saveDatas = LoadAllData();

            foreach (SaveData saveData in saveDatas) {
                Debug.Log($"Name: {saveData.name}, Last Saved: {saveData.lastSaved}");
            }
        }
    }

    public void SaveData(SaveData data) {
        // Convert game data to JSON
        string json = JsonUtility.ToJson(data);

        // Path to the save.json
        string filePath = $"{folderPath}/{data.name}.{fileFormat}";
        Debug.Log("Path: " + filePath);

        // Write JSON to file
        File.WriteAllText(filePath, json);

        Debug.Log("Game saved to: " + filePath);
        Debug.Log("Gold: " + PlayerGold.GetGoldAmount() + ", Quest ke " + (QuestManager.GetQuestIndex() + 1));

        statisticsManager.SaveCount();
    }

    public static SaveData LoadData(string filePath) {
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

    public static SaveData[] LoadAllData() {        
        try
        {
            folderPath = Application.persistentDataPath;
            // Get all the save.json file path
            string[] filePathList = Directory.GetFiles(folderPath, $"*.{fileFormat}");
            SaveData[] saveDatas = new SaveData[filePathList.Length];

            // Assign each saveData to saveDatas
            for (int i = 0; i < filePathList.Length; i++)
            {
                saveDatas[i] = LoadData(filePathList[i]);
            }

            return saveDatas;
        }
        catch (Exception e)
        {
            Debug.LogWarning("An error occurred: " + e.Message);
            return null;
        }        
    }

    public void LoadGame(string filePath) {
        SaveData gameData = LoadData(filePath);
        SceneManager.LoadScene("CobaLibri");
        questManager.SetCurrentQuest(gameData.questIndex);
        PlayerGold.SetGoldAmount(gameData.playerGold);
    }

    public static void SetLoaded() {
        isLoaded = true;
    }
}