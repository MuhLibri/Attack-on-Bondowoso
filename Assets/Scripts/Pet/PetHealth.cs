using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth = 30;

    // For Cheat Full Hp Pet
    private static bool fullHpPet = false;

    // For Cheat Kill Pet
    private static bool killPet = false;

    public Animator petAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (killPet) {
            TakeDamage(currentHealth);
        }
    }

    public void TakeDamage(int damage) {
        if(currentHealth > 0 && !fullHpPet){
            currentHealth -= damage;
            
            if (currentHealth <= 0)
            {
                if(petAnimator != null){
                    petAnimator.SetTrigger("isDead");
                }
                Die();
            }
        }
    }

    public void Die() {
        Destroy(this.gameObject, 3f);
    }

    public static bool IsFullHpPet()
    {
        return fullHpPet;
    }

    public static void ActivateFullHpPet()
    {
        fullHpPet = true;
    }

    public static void DeactivateFullHpPet()
    {
        fullHpPet = false;
    }

    public static void KillPet() {
        killPet = true;
    }
}
