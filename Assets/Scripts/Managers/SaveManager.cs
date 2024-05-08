using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public GameObject questBox;
    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        questManager = questBox.GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // TO DO make new way to save game
        if (Input.GetKeyDown(KeyCode.P)) {
            GameData gameData = new GameData(QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
            SaveGame(gameData);
        }
        
        if (Input.GetKeyDown(KeyCode.L)) {
            GameData gameData = LoadGame();
            SceneManager.LoadScene("CobaLibri");
            questManager.SetCurrentQuest(gameData.questIndex);
            PlayerGold.SetGoldAmount(gameData.playerGold);
        }
    }

    public void SaveGame(GameData data) {
        // Convert game data to JSON
        string json = JsonUtility.ToJson(data);

        // Define the file path (change this to your desired file path)
        string filePath = Application.persistentDataPath + "/savegame.json";
        Debug.Log("Path: " + filePath);

        // Write JSON to file
        File.WriteAllText(filePath, json);

        Debug.Log("Game saved to: " + filePath);
        Debug.Log("Gold: " + PlayerGold.GetGoldAmount() + ", Quest ke " + (QuestManager.GetQuestIndex() + 1));
    }

    public GameData LoadGame() {
        // Define the file path
        string filePath = Application.persistentDataPath + "/savegame.json";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read JSON from file
            string json = File.ReadAllText(filePath);

            // Convert JSON to game data object
            GameData data = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Game loaded from: " + filePath);

            return data;
        }
        else
        {
            Debug.LogWarning("No save file found at: " + filePath);
            return null;
        }
    }
}