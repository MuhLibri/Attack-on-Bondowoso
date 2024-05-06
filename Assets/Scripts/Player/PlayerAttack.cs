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
        sword = GameObject.FindGameObjectWithTag("SwordTag");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attack");
            playerAnimator.SetTrigger("Attack");
        }

        if (sword.activeSelf)
        {
            Debug.Log("Sword Equipped");
            playerAnimator.SetBool("Equip Sword", true);
        }
    }
}
