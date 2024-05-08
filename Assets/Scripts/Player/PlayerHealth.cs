using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    Animator playerAnimator;
    bool isDead;

    
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
        
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (!isDead)
        {
            currentHealth += healAmount;
            if (currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }
            healthSlider.value = currentHealth;
        }
    }

    public void Die()
    {
        isDead = true;
    }
}
