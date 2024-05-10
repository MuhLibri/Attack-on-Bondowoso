using System.Collections.Generic;
using UnityEngine;

public class AttackDown : MonoBehaviour
{
    public int attackDownPercentage = 25;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponState playerWeaponState = other.GetComponent<PlayerWeaponState>();
            if (playerWeaponState != null)
            {
                playerWeaponState.decreaseDamage(attackDownPercentage, 1);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponState playerWeaponState = other.GetComponent<PlayerWeaponState>();
            if (playerWeaponState != null)
            {
                playerWeaponState.setDefaultDamage();
            }
        }
    }
}
