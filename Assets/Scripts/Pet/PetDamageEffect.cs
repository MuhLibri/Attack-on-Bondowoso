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
        PetDamageMovement petDamageMovement = GetComponent<PetDamageMovement>();
        owner = petDamageMovement.owner;
        
    }

    void Awake()
    {
        PlayerWeaponState playerWeaponState = owner.GetComponent<PlayerWeaponState>();
        playerWeaponState.petDamageCount++;
        playerWeaponState.petDamageBoostPercentage = boostPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        PlayerWeaponState playerWeaponState = owner.GetComponent<PlayerWeaponState>();
        playerWeaponState.petDamageCount--;
        if(playerWeaponState.petDamageCount == 0){
            playerWeaponState.petDamageBoostPercentage = 0;
        }
    }
}
