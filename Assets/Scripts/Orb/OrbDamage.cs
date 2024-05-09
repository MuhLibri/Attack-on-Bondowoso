using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDamage : MonoBehaviour
{
    public int boostPercentage = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerWeaponState weaponState = other.gameObject.GetComponent<PlayerWeaponState>();
            weaponState.DamageBoost(boostPercentage);
            Destroy(this.gameObject);
        }
    }
}
