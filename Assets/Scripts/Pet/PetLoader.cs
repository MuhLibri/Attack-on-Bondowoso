using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject petAttack;
    public GameObject petHeal;
    public static int petAttackCount;
    public static int petHealCount;

    // Start is called before the first frame update
    void Start()
    {
        petAttackCount = 0;
        petHealCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPetAttack() {
        for (int i = 0; i < petAttackCount; i++) {
            GameObject pet = Instantiate(petAttack, new Vector3(), new Quaternion());
            pet.GetComponent<PetAttackMovement>().owner = player;
            pet.transform.position = player.transform.position + player.transform.forward * 3;
            pet.GetComponent<NavMeshAgent>().Warp(pet.transform.position);
        }
    }

    public void SpawnPetHeal() {
        for (int i = 0; i < petHealCount; i++) {
            GameObject pet = Instantiate(petHeal, new Vector3(300f, 10f, 200f), new Quaternion());
            pet.GetComponent<PetMovement>().owner = player;
            pet.GetComponent<NavMeshAgent>().Warp(pet.transform.position);
        }
    }
}
