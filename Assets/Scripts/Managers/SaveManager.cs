using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    public static string fileFormat = "json";
    private static string folderPath;
    public GameObject questBox;
    private QuestManager questManager;
    private static bool isLoaded = false;
    private bool insideSaveZone = false;

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
        if (isLoaded) {
            isLoaded = false;
            string filePath = $"{folderPath}/{fileName}.{fileFormat}";
            LoadGame(filePath);
        }
        // TO DO make new way to save game
        if (Input.GetKeyDown(KeyCode.P) && insideSaveZone) {
            Debug.Log("Saving in save zone");
            string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
            string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
            SaveData saveData = new SaveData(playerName, difficulty, fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
            SaveData(saveData);
        }
        
        if (Input.GetKeyDown(KeyCode.L) && insideSaveZone) {
            Debug.Log("Loading in save zone");
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

        StatisticsManager.Instance.SaveCount();
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
        PlayerPrefs.SetString("PlayerName", gameData.playerName);
        PlayerPrefs.SetString("Difficulty", gameData.difficulty);
        questManager.SetCurrentQuest(gameData.questIndex);
        PlayerGold.SetGoldAmount(gameData.playerGold);
    }

    public static void SetLoaded() {
        isLoaded = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            insideSaveZone = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            insideSaveZone = false;
        }        
    }

    private void OnGUI()
    {
        if (insideSaveZone)
        {
            float y = 60;
            GUI.Box(new Rect(0, y, Screen.width/2 - 40, 30), "Tekan P untuk save, Tekan L untuk load");
            GUI.backgroundColor = new Color(0, 0, 0, 1);
        }
    }
}