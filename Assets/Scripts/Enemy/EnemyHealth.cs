using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
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
