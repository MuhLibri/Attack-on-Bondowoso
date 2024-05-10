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
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        AdjustHealth(difficulty);
        Debug.Log("Difficulty: " + difficulty + " - Max Health: " + maxHealth.ToString());
        currentHealth = maxHealth;

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
                playerAnimator.SetLayerWeight(2, 0f);
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
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerWeaponState>().enabled = false;
        GetComponent<PlayerGold>().enabled = false;
        GetComponent<PlayerCamera>().enabled = false;
        Destroy(this.gameObject, 3f);
        GameOver.Instance.ShowGameOver();
    }

    public static bool IsNoDamage()
    {
        return noDamage;
    }

    public static void ActivateNoDamage()
    {
        noDamage = true;
    }

    public static void DeactivateNoDamage()
    {
        noDamage = false;
    }

    void AdjustHealth(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                maxHealth = 1000;
                break;
            case "Medium":
                maxHealth = 500;
                break;
            case "Hard":
                maxHealth = 250;
                break;
            default:
                maxHealth = 1000;
                break;
        }
    }
}
