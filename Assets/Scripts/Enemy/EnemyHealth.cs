using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!IsDead())
        {
            currentHealth -= damage;
            if (IsDead())
            {
                Die();
            }
        }
    }

    public bool IsDead()
    {
        return (currentHealth <= 0f);
    }

    void Die()
    {
        QuestManager.AddKilled();
        Destroy(this.gameObject);
    }
}
