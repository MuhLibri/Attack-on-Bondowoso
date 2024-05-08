using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int questIndex;
    public int playerGold;
    
    public GameData(int questIndex, int playerGold) {
        this.questIndex = questIndex;
        this.playerGold = playerGold;
    }
}
