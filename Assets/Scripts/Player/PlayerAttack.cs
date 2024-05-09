using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float swordAttackCooldownTime = 1f;
    public Animator playerAnimator;
    public List<AudioClip> audioClips;

    private float lastAttackTime;
    private bool audioPlayed = false;
    private AudioSource audioSource;
    StatisticsManager statisticsManager;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = Time.time;
        audioSource = GetComponent<AudioSource>();
        statisticsManager = GetComponent<StatisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Attack 1") && !audioSource.isPlaying && !audioPlayed)
        {
            audioSource.PlayOneShot(audioClips[0]);
            audioPlayed = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (playerAnimator.GetBool("Equip Sword") == true)
            {
                if (Time.time - lastAttackTime > swordAttackCooldownTime)
                {
                    lastAttackTime = Time.time;
                    playerAnimator.SetTrigger("Attack");
                    audioPlayed = false;
                }
            }
            else
            {
                playerAnimator.SetTrigger("Attack");
                statisticsManager.ShotFired(5);
            }
        }
        else
        {
            playerAnimator.ResetTrigger("Attack");
        }
    }
}
