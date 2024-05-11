using UnityEngine;

public class DOT : MonoBehaviour
{
    public int damagePerSecond = 10;

    private bool isDamaging = false;
    private float lastDamageTime;
    private Collider playerCollider;

    void Start()
    {
        lastDamageTime = Time.time;
    }

    void Update()
    {
        // Check if it's time to damage again
        if (isDamaging && Time.time - lastDamageTime > 1f)
        {
            DamageOverTime();
            lastDamageTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
            isDamaging = true;
            lastDamageTime = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
            isDamaging = false;
        }
    }

    void DamageOverTime()
    {
        if (playerCollider == null) return;

        // Assuming you've set damage on your projectiles
        PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
        PetHealth petHealth = playerCollider.GetComponent<PetHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond);
            petHealth.TakeDamage(damagePerSecond);
        }
    }
}
