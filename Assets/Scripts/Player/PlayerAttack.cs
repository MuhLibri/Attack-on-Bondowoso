using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float swordAttackCooldownTime = 1f;
    public Animator playerAnimator;

    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (playerAnimator.GetBool("Equip Sword") == true)
            {
                if (Time.time - lastAttackTime > swordAttackCooldownTime)
                {
                    lastAttackTime = Time.time;
                    playerAnimator.SetTrigger("Attack");
                }
            }
            else
            {
                playerAnimator.SetTrigger("Attack");
            }
        }
        else
        {
            playerAnimator.ResetTrigger("Attack");
        }
    }
}
