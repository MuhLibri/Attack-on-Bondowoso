using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    public GameObject player;

    private Transform container;
    private Transform shopItemTemplate;
    private TextMeshProUGUI goldText;
    private Color originalGoldTextColor;
    private Coroutine blinkCoroutine;


    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("itemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        goldText = transform.Find("goldText").gameObject.GetComponent<TextMeshProUGUI>();
        originalGoldTextColor = goldText.color;
    }

    private void Start()
    {
        CreateShopItem(ShopList.ShopItem.PetHeal);
        CreateShopItem(ShopList.ShopItem.PetAttack);
        PlayerGold.GiveGold(100);
        goldText.SetText(PlayerGold.GetGoldAmount().ToString());
    }

    private void CreateShopItem(ShopList.ShopItem shopItem)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform rectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 50f;
        rectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * (container.childCount - 1));

        int cost = ShopList.GetCost(shopItem);
        string name = ShopList.GetName(shopItem);

        shopItemTransform.Find("name").GetComponent<TMPro.TextMeshProUGUI>().SetText(name);
        shopItemTransform.Find("cost").GetComponent<TMPro.TextMeshProUGUI>().SetText(cost.ToString());

        shopItemTransform.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuyItem(shopItem);
        });

        shopItemTransform.gameObject.SetActive(true);
    }

    public void BuyItem(ShopList.ShopItem shopItem)
    {
        int cost = ShopList.GetCost(shopItem);
        if (cost <= PlayerGold.GetGoldAmount())
        {
            PlayerGold.TakeGold(cost);
            goldText.SetText(PlayerGold.GetGoldAmount().ToString());
            GameObject pet = Instantiate(ShopList.GetPrefab(shopItem));
            if(shopItem.ToString() == "PetHeal")
            {
                pet.GetComponent<PetMovement>().owner = player;
            }
            else if(shopItem.ToString() == "PetAttack")
            {
                pet.GetComponent<PetAttackMovement>().owner = player;
            }
            pet.transform.position = player.transform.position + Vector3.forward * 5f;
            Debug.Log("Bought " + ShopList.GetName(shopItem));
        }
        else
        {
            Debug.Log("Not enough money to buy " + ShopList.GetName(shopItem));
            if(blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
    }

    public IEnumerator BlinkCoroutine()
    {
        for(int i = 0; i < 5; i++)
        {
            goldText.color = goldText.color == Color.red ? originalGoldTextColor : Color.red;
            yield return new WaitForSeconds(0.15f);
        }
        goldText.color = originalGoldTextColor;
    }
}
