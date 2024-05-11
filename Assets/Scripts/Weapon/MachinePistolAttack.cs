using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePistolAttack : MonoBehaviour
{
    public float accuracy;
    public int defaultDamage = 10;
    public GameObject owner;
    public int damage = 10;
    public GameObject projectilePrefab;
    public Transform projectilePoint;
    public float shotForce;
    public float cooldownTime;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletShell;
    public List<AudioClip> audioClips;
    public Animator playerAnimator;
    float lastShotTime;
    AudioSource audioSource;

    void Start()
    {
        lastShotTime = -cooldownTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - lastShotTime >= cooldownTime)
        {
            if (Input.GetButton("Fire1") && (playerAnimator.GetBool("Aim") == true))
            {
                muzzleFlash.Play();
                bulletShell.Play();
                Shoot();
            }
            else
            {
                muzzleFlash.Stop();
                bulletShell.Stop();

            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, projectilePoint.rotation);
        projectile.GetComponent<ProjectileController>().damage = damage;
        projectile.GetComponent<ProjectileController>().ownerTag = owner.tag;
        if (owner.CompareTag("Player")) StatisticsManager.Instance.ShotFired();

        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = projectilePoint.forward;
        direction.x += Random.Range(-accuracy, accuracy);
        direction.y += Random.Range(-accuracy, accuracy);
        direction.z += Random.Range(-accuracy, accuracy);

        rb.AddForce(direction * shotForce, ForceMode.VelocityChange);
        lastShotTime = Time.time;
    }
}
