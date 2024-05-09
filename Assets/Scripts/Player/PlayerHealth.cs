using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (!IsDead())
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (IsDead())
            {
                Die();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (!IsDead())
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthSlider.value = currentHealth;
        }
    }

    public bool IsDead()
    {
        return (currentHealth <= 0f);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
