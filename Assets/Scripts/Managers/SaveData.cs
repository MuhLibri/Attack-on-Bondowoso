using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveData
{
    public string playerName;
    public string difficulty;
    public string name;
    public string lastSaved;
    public int questIndex;
    public int playerGold;
    
    public SaveData(string playerName, string difficulty, string name, int questIndex, int playerGold) {
        this.playerName = playerName;
        this.difficulty = difficulty;
        this.name = name != "" ? name : "Save1";
        this.lastSaved = DateTime.Now.ToString();
        this.questIndex = questIndex;
        this.playerGold = playerGold;
    }
}
