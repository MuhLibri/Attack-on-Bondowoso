using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    private static string fileName = "Save1";
    public static string fileFormat = "json";
    private static string folderPath;
    public GameObject questBox;
    public GameObject healthBar;
    private QuestManager questManager;
    private static bool isLoaded = false;
    private bool insideSaveZone = false;
    public GameObject savePanel;

    // Start is called before the first frame update
    void Start()
    {
        folderPath = Application.persistentDataPath;
        questManager = questBox.GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoaded) {
            isLoaded = false;
            string filePath = $"{folderPath}/{fileName}.{fileFormat}";
            LoadGame(filePath);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && insideSaveZone) {
            Debug.Log("Accessing save panel");
            questBox.SetActive(savePanel.activeSelf);
            healthBar.SetActive(savePanel.activeSelf);
            savePanel.SetActive(!savePanel.activeSelf);
            Cursor.lockState = savePanel.activeSelf? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public static void SaveData(SaveData data) {
        // Convert game data to JSON
        string json = JsonUtility.ToJson(data);

        // Path to the save.json
        string filePath = $"{folderPath}/{data.saveDataName}.{fileFormat}";
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
            string[] filePathList = Directory.GetFiles(folderPath, $"Save*.{fileFormat}");
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
        // TO DO load Main
        SaveData gameData = LoadData(filePath);
        SceneManager.LoadScene("CobaLibri");
        PlayerPrefs.SetString("PlayerName", gameData.playerName);
        PlayerPrefs.SetString("Difficulty", gameData.difficulty);
        questManager.SetCurrentQuest(gameData.questIndex);
        PlayerGold.SetGoldAmount(gameData.playerGold);
    }

    public static void SetLoaded(string fileName) {
        isLoaded = true;
        SaveManager.fileName = fileName;
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
        if (insideSaveZone && !savePanel.activeSelf)
        {
            float y = 60;
            GUI.Box(new Rect(0, y, Screen.width/2 - 40, 30), "Press Tab button to save you progress");
            GUI.backgroundColor = new Color(0, 0, 0, 1);
        }
    }
}