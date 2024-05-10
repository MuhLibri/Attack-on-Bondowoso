using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using TMPro;
using UnityEngine;

public class SavePanel : MonoBehaviour
{
    // Objects in Load Save Menu
    public TextMeshProUGUI saveOneText;
    public TextMeshProUGUI saveTwoText;
    public TextMeshProUGUI saveThreeText;

    // Start is called before the first frame update
    void Start()
    {
        SaveData[] saveDatas = SaveManager.LoadAllData();

        foreach (SaveData saveData in saveDatas) {
            if (saveData.saveSlot == 1) {
                saveOneText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
            }
            else if (saveData.saveSlot == 2) {
                saveTwoText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
            }
            else if (saveData.saveSlot == 3) {
                saveThreeText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveToSlot1() {
        string fileName = "Save1";
        Debug.Log("Saving in save zone");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        SaveData saveData = new SaveData(fileName, 1, playerName, difficulty, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveOneText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
    }

    public void SaveToSlot2() {
        string fileName = "Save2";
        Debug.Log("Saving in save zone");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        SaveData saveData = new SaveData(fileName, 2, playerName, difficulty, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveTwoText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
    }

    public void SaveToSlot3() {
        string fileName = "Save3";
        Debug.Log("Saving in save zone");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        SaveData saveData = new SaveData(fileName, 3, playerName, difficulty, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveThreeText.text = $"{saveData.saveDataName} - {saveData.lastSaved}";
    }
}
