using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject orbDamage;
    public GameObject orbHealth;
    public GameObject orbSpeed;
    private GameObject[] orbs;
    public List<AudioClip> audioClips;
    public Animator enemyAnimator;

    private AudioSource audioSource;
    private static bool oneHitKill = false;

    // Start is called before the first frame update
    void Start()
    {
        orbs = new GameObject[3];
        orbs[0] = orbDamage;
        orbs[1] = orbHealth;
        orbs[2] = orbSpeed;
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!IsDead()) {
            currentHealth -= !oneHitKill? damage : currentHealth;
            if(audioClips.Count > 0){
                audioSource.PlayOneShot(audioClips[0]);
            }
            
            if (IsDead())
            {
                if(audioClips.Count > 1){
                    audioSource.PlayOneShot(audioClips[1]);
                }
                enemyAnimator.SetTrigger("isDead");
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
        if (IsOrbDrop()) {
            GameObject chosenOrb = RandomizeOrb();
            Instantiate(chosenOrb, transform.position, transform.rotation);
        }
        Destroy(this.gameObject, 3f);
    }

    bool IsOrbDrop() {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 2);

        return randomNumber == 0 ? true : false;
    }

    GameObject RandomizeOrb() {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 3);        

        return orbs[randomNumber];
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
