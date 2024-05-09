using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public List<AudioClip> audioClips;
    public Animator enemyAnimator;

    private AudioSource audioSource;
    private static bool oneHitKill = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!oneHitKill) {
            if (!IsDead())
            {
                currentHealth -= damage;
                audioSource.PlayOneShot(audioClips[0]);
                if (IsDead())
                {
                    audioSource.PlayOneShot(audioClips[1]);
                    enemyAnimator.SetTrigger("isDead");
                    Die();
                }
            }
        }
        else {
            Debug.Log("One hit kill Mati");
            currentHealth -= currentHealth;
            audioSource.PlayOneShot(audioClips[0]);
            audioSource.PlayOneShot(audioClips[1]);
            enemyAnimator.SetTrigger("isDead");
            Die();
        }
    }

    public bool IsDead()
    {
        return (currentHealth <= 0f);
    }

    void Die()
    {
        QuestManager.AddKilled();
        Destroy(this.gameObject, 3f);
    }

    public static bool IsOneHitKil() {
        return oneHitKill;
    }

    public static void ActivateOneHitKill() {
        oneHitKill = true;
    }

    public static void DeactivateOneHitKill() {
        oneHitKill = false;
    }
}
