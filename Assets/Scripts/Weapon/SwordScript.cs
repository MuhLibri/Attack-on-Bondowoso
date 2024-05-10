using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public int defaultDamage = 25;
    public int damage = 25;
    public string exceptionTag;
    public Animator animator;

    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (exceptionTag == "Player" && other.CompareTag("Enemy"))
        {
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Attack 1")) && (Time.time - lastAttackTime > 0.2))
            {
                lastAttackTime = Time.time;
                // Assuming you've set damage on your projectiles
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
        else if (exceptionTag == "Enemy")
        {
            if (animator.GetBool("isAttacking") && (Time.time - lastAttackTime > 1.5))
            {
                if(other.CompareTag("Player")) {
                    lastAttackTime = Time.time;
                    // Assuming you've set damage on your projectiles
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                    }
                
                }
                else if(other.CompareTag("Pet")) {
                    lastAttackTime = Time.time;
                    // Assuming you've set damage on your projectiles
                    PetHealth petHealth = other.GetComponent<PetHealth>();
                    if (petHealth != null)
                    {
                        petHealth.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
