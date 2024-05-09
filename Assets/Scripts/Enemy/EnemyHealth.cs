using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public List<AudioClip> audioClips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!IsDead())
        {
            currentHealth -= damage;
            audioSource.PlayOneShot(audioClips[0]);
            if (IsDead())
            {
                audioSource.PlayOneShot(audioClips[1]);
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
