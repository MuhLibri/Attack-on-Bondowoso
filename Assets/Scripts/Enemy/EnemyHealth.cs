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

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (!IsDead())
        {
            Debug.Log("Nyawa: " + currentHealth);
            currentHealth -= damage;

            if (IsDead())
            {
                Debug.Log("Mati");
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
