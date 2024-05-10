using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public GameObject uiShop;
    private bool isActiveShop = false;
    private GameObject player;
    private void Update()
    {
        if(player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!isActiveShop)
            {
                uiShop.SetActive(true);
                uiShop.GetComponent<UI_Shop>().player = player;
                isActiveShop = true;
                player.GetComponent<PlayerCamera>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerAttack>().enabled = false;
                player.GetComponent<PlayerWeaponState>().enabled = false;
                player.GetComponent<StatisticsManager>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                uiShop.SetActive(false);
                uiShop.GetComponent<UI_Shop>().player = null;
                isActiveShop = false;
                player.GetComponent<PlayerCamera>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerAttack>().enabled = true;
                player.GetComponent<PlayerWeaponState>().enabled = true;
                player.GetComponent<StatisticsManager>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }
}
