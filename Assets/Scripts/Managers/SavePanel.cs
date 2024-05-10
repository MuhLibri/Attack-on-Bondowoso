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
        Debug.Log("Banyak: " + saveDatas.Length);

        for (int i = 0; i < saveDatas.Length; i++) {
            if (i == 0) {
                saveOneText.text = $"{saveDatas[i].name} - {saveDatas[i].lastSaved}";
            }
            else if (i == 1) {
                saveTwoText.text = $"{saveDatas[i].name} - {saveDatas[i].lastSaved}";
            }
            else if (i == 2) {
                saveThreeText.text = $"{saveDatas[i].name} - {saveDatas[i].lastSaved}";
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
        SaveData saveData = new SaveData(playerName, difficulty, fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveOneText.text = $"{saveData.name} - {saveData.lastSaved}";
    }

    public void SaveToSlot2() {
        string fileName = "Save2";
        Debug.Log("Saving in save zone");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        SaveData saveData = new SaveData(playerName, difficulty, fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveTwoText.text = $"{saveData.name} - {saveData.lastSaved}";
    }

    public void SaveToSlot3() {
        string fileName = "Save3";
        Debug.Log("Saving in save zone");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        SaveData saveData = new SaveData(playerName, difficulty, fileName, QuestManager.GetQuestIndex(), PlayerGold.GetGoldAmount());
        SaveManager.SaveData(saveData);
        saveThreeText.text = $"{saveData.name} - {saveData.lastSaved}";
    }
}
