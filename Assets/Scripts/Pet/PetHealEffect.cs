using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealEffect : MonoBehaviour
{
    public int healAmount;
    public int effectInterval;
    public float effectRadius;

    GameObject owner;
    PetMovement petMovement;

    PlayerHealth playerHealth;
    void Start()
    {
        petMovement = GetComponent<PetMovement>();
        owner = petMovement.owner;
        playerHealth = owner.GetComponent<PlayerHealth>();
    }

    void Awake() {
        InvokeRepeating("HealEffect", effectInterval, effectInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HealEffect() {
        if (Vector3.Distance(transform.position, owner.transform.position) <= effectRadius) {
            playerHealth.Heal(healAmount);
        }
    }

    
}
