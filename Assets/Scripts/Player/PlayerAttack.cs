using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject sword;

    public Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {        
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weapons)
        {
            if (weapon.name == "Sword")
            {
                sword = weapon;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attack");
            playerAnimator.SetTrigger("Attack");
        }
        else {
            playerAnimator.ResetTrigger("Attack");
        }
    }
}
