using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private string fileFormat = "json";
    private string folderPath;
    public GameObject questBox;
    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        folderPath = Application.persistentDataPath;
        questManager = questBox.GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string fileName = "Save1";
        // TO DO make new way to save game
        if (Input.GetKeyDown(KeyCode.P)) {
            SaveData saveData = new SaveData(fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
            SaveData(saveData);
        }
        
        if (Input.GetKeyDown(KeyCode.L)) {
            string filePath = $"{folderPath}/{fileName}.{fileFormat}";
            SaveData gameData = LoadData(filePath);
            SceneManager.LoadScene("CobaLibri");
            questManager.SetCurrentQuest(gameData.questIndex);
            PlayerGold.SetGoldAmount(gameData.playerGold);
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

        // Define the file path (change this to your desired file path)
        string filePath = $"{folderPath}/{data.name}.{fileFormat}";
        Debug.Log("Path: " + filePath);

        // Write JSON to file
        File.WriteAllText(filePath, json);

        Debug.Log("Game saved to: " + filePath);
        Debug.Log("Gold: " + PlayerGold.GetGoldAmount() + ", Quest ke " + (QuestManager.GetQuestIndex() + 1));
    }

    public SaveData LoadData(string filePath) {
        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read JSON from file
            string json = File.ReadAllText(filePath);

            // Convert JSON to game data object
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

    public SaveData[] LoadAllData() {        
        try
        {
            // Get all file names in the directory
            string[] filePathList = Directory.GetFiles(folderPath, $"*.{fileFormat}");
            SaveData[] saveDatas = new SaveData[filePathList.Length];

            // Print the file names
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
}