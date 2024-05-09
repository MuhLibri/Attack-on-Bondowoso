using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    private static int goldAmount = 0;
    private static bool motherlode = false;

    public static int GetGoldAmount() {
        return goldAmount;
    }

    public static void SetGoldAmount(int goldAmount) {
        PlayerGold.goldAmount = goldAmount;
        Debug.Log("Gold: " + PlayerGold.goldAmount);
    }

    // Give gold to player based on input amount
    public static void GiveGold(int amount) {
        goldAmount += amount;
        Debug.Log("Gold Player: " + goldAmount);
    }

    // Take gold from player on input amount
    public static void TakeGold(int amount) {
        goldAmount -= !motherlode? amount : 0;
    }

    public static bool IsMotherlode() {
        return motherlode;
    }

    public static void ActivateMotherlode() {
        motherlode = true;
    }

    public static void DeactivateMotherlode() {
        motherlode = false;
    }
}
