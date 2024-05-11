using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopList
{
    public enum ShopItem
    {
        PetHeal,
        PetAttack
    }

    public static int GetCost(ShopItem item)
    {
        switch (item)
        {
            case ShopItem.PetHeal:
                return 2000;
            case ShopItem.PetAttack:
                return 2000;
            default:
                return 0;
        }
    }

    public static string GetName(ShopItem item)
    {
        switch (item)
        {
            case ShopItem.PetHeal:
                return "Healer Pet";
            case ShopItem.PetAttack:
                return "Attacker Pet";
            default:
                return "";
        }
    }

    public static GameObject GetPrefab(ShopItem item)
    {
        switch (item)
        {
            case ShopItem.PetHeal:
                return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Pet/PetHeal.prefab");
            case ShopItem.PetAttack:
                return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Pet/PetAttack.prefab");
            default:
                return null;
        }
        return null;
    }
}
