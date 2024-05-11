using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopList: MonoBehaviour
{
    public GameObject[] petPrefabs;
    public static ShopList Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                return ShopList.Instance.petPrefabs[0];
            case ShopItem.PetAttack:
                return ShopList.Instance.petPrefabs[1];
            default:
                return null;
        }
        return null;
    }
}
