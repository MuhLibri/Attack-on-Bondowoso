using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDamageEffect : MonoBehaviour
{
    public int boostPercentage = 10;
    private GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        PetDamageMovement petDamageMovement = GetComponent<PetDamageMovement>();
        owner = petDamageMovement.owner;

        if(owner.tag == "Player"){
            PlayerWeaponState playerWeaponState = owner.GetComponent<PlayerWeaponState>();
            playerWeaponState.petDamageCount++;
            playerWeaponState.petDamageBoostPercentage = boostPercentage;
        }
        else if (owner.tag == "Enemy"){
            PetDamageManager petDamageManager = owner.GetComponent<PetDamageManager>();
            petDamageManager.petDamageCount++;
            petDamageManager.petDamageBoostPercentage = boostPercentage;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        if(owner.tag == "Player"){
            PlayerWeaponState playerWeaponState = owner.GetComponent<PlayerWeaponState>();
            playerWeaponState.petDamageCount--;
            if(playerWeaponState.petDamageCount == 0){
                playerWeaponState.petDamageBoostPercentage = 0;
            }
        }
        else if (owner.tag == "Enemy"){
            PetDamageManager petDamageManager = owner.GetComponent<PetDamageManager>();
            petDamageManager.petDamageCount--;
            if(petDamageManager.petDamageCount == 0){
                petDamageManager.petDamageBoostPercentage = 0;
            }
        }
       
    }
}
