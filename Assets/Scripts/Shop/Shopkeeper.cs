using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public GameObject uiShop;
    public GameObject shopAlertUI;
    public GameObject shopAlertFollow;
    public TextMeshPro timerAlert;

    private bool isShopOpen = true;
    private bool isActiveShop = false;
    private GameObject player;
    private Quest currentQuest;
    private void Start()
    {
        currentQuest = QuestManager.currentQuest;
    }
    private void Update()
    {
        if(currentQuest == null || currentQuest != QuestManager.currentQuest)
        {
            currentQuest = QuestManager.currentQuest;
            StartCoroutine(openShop(30));
        }
        if (!isShopOpen) return;

        if(player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!isActiveShop)
            {
                activateShop();
            }
            else
            {
                deactivateShop();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            activateAlert();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
            deactivateAlert();
        }
    }
    private void activateAlert()
    {
        if(!isShopOpen) return;
        shopAlertUI.SetActive(true);
        shopAlertFollow.GetComponent<ShopAlertFollow>().player = player;
        shopAlertFollow.SetActive(true);
    }

    private void deactivateAlert()
    {
        shopAlertUI.SetActive(false);
        shopAlertFollow.SetActive(false);
        shopAlertFollow.GetComponent<ShopAlertFollow>().player = null;
    }

    private void activateShop()
    {
        uiShop.GetComponent<UI_Shop>().Show();
        uiShop.GetComponent<UI_Shop>().player = player;
        isActiveShop = true;
        if(player != null) { 
            player.GetComponent<PlayerCamera>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponent<PlayerWeaponState>().enabled = false;
            player.GetComponent<StatisticsManager>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            deactivateAlert();
        }
    }

    private void deactivateShop()
    {
        uiShop.GetComponent<UI_Shop>().Hide();
        uiShop.GetComponent<UI_Shop>().player = null;
        isActiveShop = false;
        if(player != null) {
            player.GetComponent<PlayerCamera>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttack>().enabled = true;
            player.GetComponent<PlayerWeaponState>().enabled = true;
            player.GetComponent<StatisticsManager>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            activateAlert();
        }
    }

    private IEnumerator openShop(int time)
    {
        timerAlert.text = time.ToString();
        isShopOpen = true;
        Debug.Log("opening shop");
        while(time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            timerAlert.text = (time).ToString();
        }
        isShopOpen = false;
        if(isActiveShop)
        {
            deactivateShop();
            deactivateAlert();
        }
    }
}
