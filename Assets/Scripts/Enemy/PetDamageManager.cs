using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDamageManager : MonoBehaviour
{
    public int petDamageCount;
    public int petDamageBoostPercentage;
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.CompareTag("Weapon"))
            {
                weapon = child.gameObject;
                break;
            }
        }
    }

    void setDefaultDamage() {
        if(weapon.name == "Sword"){
            SwordScript swordScript = weapon.GetComponent<SwordScript>();
            swordScript.damage = swordScript.defaultDamage;
        }
        else if(weapon.name == "Shotgun") {
            ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
            shotgunAttack.damage = shotgunAttack.defaultDamage;
        }
    }

    void addDamage(int percentage, int multiplier) {
        int percentageDamange = percentage * multiplier;

        if(weapon.name == "Sword"){
            SwordScript swordScript = weapon.GetComponent<SwordScript>();
            int damageBoost = swordScript.defaultDamage * percentage / 100;
            swordScript.damage += damageBoost;
        }
        else if(weapon.name == "Shotgun") {
            ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
            int damageBoost = shotgunAttack.defaultDamage * percentage / 100;
            shotgunAttack.damage += damageBoost;
        }
    }

    void petDamageBoost(){
        if(petDamageCount > 0){
            addDamage(petDamageBoostPercentage, petDamageCount);
        }
        else{
            setDefaultDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        petDamageBoost();
    }
}
