using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public List<AudioClip> audioClips;
    public CinemachineVirtualCamera deathCam;

    private Animator playerAnimator;
    private AudioSource audioSource;

    // For Cheat No Damage
    private static bool noDamage = false;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (!IsDead() && !noDamage)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            audioSource.PlayOneShot(audioClips[0]);
            if (IsDead())
            {
                audioSource.PlayOneShot(audioClips[1]);
                playerAnimator.SetTrigger("Dead");
                deathCam.m_Lens.FieldOfView = 100;
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
        Destroy(this.gameObject, 3f);
    }

    public static bool IsNoDamage() {
        return noDamage;
    }

    public static void ActivateNoDamage() {
        noDamage = true;
    }

    public static void DeactivateNoDamage() {
        noDamage = false;
    }
}
