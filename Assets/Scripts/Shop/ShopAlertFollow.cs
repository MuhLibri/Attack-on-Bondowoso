using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAlertFollow : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Vector3 directionToPlayer = transform.position - player.transform.position;
        directionToPlayer.y = 0;

        Quaternion toRotation = Quaternion.LookRotation(directionToPlayer);

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.rotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);
        }
    }
}
