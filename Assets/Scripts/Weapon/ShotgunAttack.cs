using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    public GameObject owner;
    public int defaultDamage = 10;
    public int damage;

    public int ammo;
    public int projectilesPerShot = 5;
    [Range(0.01f, 1.0f)]
    public float accuracy;
    public GameObject ammoPrefab;
    public GameObject projetilePrefab;
    public GameObject muzzlePrefab;
    public Transform shotPoint;
    public Transform projectilePoint;
    public Transform muzzlePoint;
    public float shotForce;
    public float recoilForce;
    public float recoveryTime;
    public float cooldownTime;
    public List<AudioClip> audioClips;
    public Animator playerAnimator;

    int maxAmmoShell = 5;
    Queue<GameObject> ammoShells = new Queue<GameObject>();
    float lastShotTime;
    Vector3 originalShotgunPosition;
    Quaternion originalShotgunRotation;
    bool isRecoiling = false;
    AudioSource audioSource;
    float maxShootingAngleX = 40f;
    float maxShootingAngleY = 40f;
    // Start is called before the first frame update
    void Start()
    {
        damage = defaultDamage;
        lastShotTime = -cooldownTime;

        originalShotgunPosition = transform.localPosition;
        originalShotgunRotation = transform.localRotation;

        maxShootingAngleX *= (1.0f - accuracy);
        maxShootingAngleY *= (1.0f - accuracy);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShotTime >= cooldownTime)
        {
            if (Input.GetButton("Fire1") && owner.CompareTag("Player") && playerAnimator.GetBool("Aim"))
            {
                Shoot();
            }
            else if (owner.CompareTag("Enemy") && owner.GetComponent<EnemyMovement>().IsAttacking() && !owner.GetComponent<EnemyMovement>().IsRunningAway() && !owner.GetComponent<EnemyHealth>().IsDead())
            {
                Shoot();
            }
        }

        if (isRecoiling)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalShotgunPosition, Time.deltaTime / recoveryTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalShotgunRotation, Time.deltaTime / recoveryTime);
            if (Vector3.Distance(transform.localPosition, originalShotgunPosition) < 0.001f)
            {
                isRecoiling = false;
                transform.localPosition = originalShotgunPosition;
                transform.localRotation = originalShotgunRotation;
            }
        }

    }

    void Shoot()
    {
        GameObject flash = Instantiate(muzzlePrefab, muzzlePoint.position, muzzlePoint.rotation);
        flash.transform.SetParent(transform);

        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);

        for (int i = 0; i < projectilesPerShot; i++)
        {
            Quaternion newRotation = Quaternion.Euler(Random.Range(-maxShootingAngleX, maxShootingAngleX), Random.Range(-maxShootingAngleY, maxShootingAngleY), 0f);
            GameObject projectile = Instantiate(projetilePrefab, projectilePoint.position, projectilePoint.rotation * newRotation);
            projectile.GetComponent<ProjectileController>().damage = damage;
            projectile.GetComponent<ProjectileController>().ownerTag = owner.tag;
        }

        GameObject ammo;
        Rigidbody rb;
        if (ammoShells.Count >= maxAmmoShell)
        {
            ammo = ammoShells.Dequeue();
            ammo.transform.localPosition = shotPoint.position;
            ammo.transform.rotation = shotPoint.rotation;

            rb = ammo.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
        }
        else
        {
            ammo = Instantiate(ammoPrefab, shotPoint.position, shotPoint.rotation);

            rb = ammo.GetComponent<Rigidbody>();
        }
        ammoShells.Enqueue(ammo);

        rb.AddForce(shotPoint.forward * shotForce, ForceMode.Impulse);
        lastShotTime = Time.time;

        Recoil();
        StatisticsManager.Instance.ShotFired(projectilesPerShot);
    }

    void Recoil()
    {
        isRecoiling = true;

        transform.Translate(-Vector3.forward * recoilForce);
        transform.Rotate(Vector3.left * 20);
    }
}
