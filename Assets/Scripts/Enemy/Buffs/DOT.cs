using UnityEngine;

public class DOT : MonoBehaviour
{
    public int damagePerSecond = 10;

    private bool isDamagingPlayer = false;
    private bool isDamagingPet = false;
    private float lastDamageTimePlayer;
    private float lastDamageTimePet;
    private Collider playerCollider;
    private Collider petCollider;

    void Start()
    {
        lastDamageTimePlayer = Time.time;
        lastDamageTimePet = Time.time;
    }

    void Update()
    {
        if (isDamagingPlayer && Time.time - lastDamageTimePlayer > 1f)
        {
            DamageOverTime("Player");
            lastDamageTimePlayer = Time.time;
        }

        if (isDamagingPet && Time.time - lastDamageTimePet > 1f)
        {
            DamageOverTime("Pet");
            lastDamageTimePet = Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
            isDamagingPlayer = true;
            lastDamageTimePlayer = Time.time;
        }

        if (other.CompareTag("Pet"))
        {
            petCollider = other;
            isDamagingPet = true;
            lastDamageTimePet = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
            isDamagingPlayer = false;
        }

        if (other.CompareTag("Pet"))
        {
            petCollider = null;
            isDamagingPet = false;
        }
    }

    void DamageOverTime(string damagedObject)
    {
        if (playerCollider == null) return;
        if (petCollider == null) return;

        if (damagedObject == "Player")
        {
            // Assuming you've set damage on your projectiles
            if (playerCollider.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.TakeDamage(damagePerSecond);
            }
        }
        else if (damagedObject == "Pet")
        {
            // Assuming you've set damage on your projectiles
            if (petCollider.TryGetComponent<PetHealth>(out var petHealth))
            {
                petHealth.TakeDamage(damagePerSecond);
            }
        }
    }
}
