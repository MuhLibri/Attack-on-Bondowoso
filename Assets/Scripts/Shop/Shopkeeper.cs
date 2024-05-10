using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public GameObject uiShop;
    public GameObject shopAlert;
    private bool isActiveShop = false;
    private GameObject player;
    private void Update()
    {
        if(player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!isActiveShop)
            {
                uiShop.GetComponent<UI_Shop>().Show();
                uiShop.GetComponent<UI_Shop>().player = player;
                isActiveShop = true;

            }
            else
            {
                uiShop.GetComponent<UI_Shop>().Hide();
                uiShop.GetComponent<UI_Shop>().player = null;
                isActiveShop = false;

            }
        }
        if (isActiveShop && player != null)
        {
            player.GetComponent<PlayerCamera>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponent<PlayerWeaponState>().enabled = false;
            player.GetComponent<StatisticsManager>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            shopAlert.gameObject.SetActive(false);
        }
        else if (isActiveShop == false && player != null)
        {
            player.GetComponent<PlayerCamera>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttack>().enabled = true;
            player.GetComponent<PlayerWeaponState>().enabled = true;
            player.GetComponent<StatisticsManager>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            shopAlert.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shopAlert.gameObject.SetActive(true);
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shopAlert.gameObject.SetActive(false);
            player = null;
        }
    }
}
