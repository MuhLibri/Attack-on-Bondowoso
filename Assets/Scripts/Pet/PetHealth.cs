using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth = 30;

    public Animator petAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage) {
        if(currentHealth > 0){
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
}
