using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveData
{
    public string saveDataName;
    public string lastSaved;
    public int saveSlot;
    public string playerName;
    public string difficulty;
    public int questIndex;
    public int playerGold;
    
    public SaveData(string saveDataName, int saveSlot, string playerName, string difficulty, int questIndex, int playerGold) {
        this.saveDataName = saveDataName != "" ? saveDataName : "Save1";
        this.lastSaved = DateTime.Now.ToString();
        this.saveSlot = saveSlot;
        this.playerName = playerName;
        this.difficulty = difficulty;
        this.questIndex = questIndex;
        this.playerGold = playerGold;
    }
}
