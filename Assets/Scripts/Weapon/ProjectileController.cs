using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float maxDistance = 400f;
    public float damage = 10f;
    public float speed = 100f;
    public AudioClip bulletHit;
    public AudioClip bulletPass;
    public string ownerTag;
    public bool isShotgunBullet = false;

    private float traveledDistance;
    private AudioSource audioSource;
    private Rigidbody rb;
    void Start()
    {
        traveledDistance = 0f;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(bulletPass);
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        traveledDistance += Time.deltaTime * speed;
        if (traveledDistance > maxDistance) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ownerTag == "Player" && other.CompareTag("Enemy")) {
            if(isShotgunBullet) damage = damage * (2 - 0.005f*(traveledDistance * traveledDistance)); // 2-0.005x^2
            if (damage < 0) damage = 0;
            Debug.Log("Damage dealt: " + damage);

            if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage((int)damage);
            }
            else if (other.TryGetComponent<PetHealth>(out var petHealth)){
                petHealth.TakeDamage((int)damage);
            }

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.localScale = Vector3.zero;
            
            Destroy(this.gameObject, bulletHit.length);
            StatisticsManager.Instance.ShotHit();
        }
        else if(ownerTag == "Enemy"){
            if(other.CompareTag("Player")) { 

                other.GetComponent<PlayerHealth>().TakeDamage((int)damage);
                // audioSource.Stop();
                // audioSource.PlayOneShot(bulletHit);
                // audioSource.loop = false;

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.localScale = Vector3.zero;
                
                Destroy(this.gameObject, bulletHit.length);
            }
            if(other.CompareTag("Pet")) {
                other.GetComponent<PetHealth>().TakeDamage((int)damage);
                // audioSource.Stop();
                // audioSource.PlayOneShot(bulletHit);
                // audioSource.loop = false;

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.localScale = Vector3.zero;
                
                Destroy(this.gameObject, bulletHit.length);
            }
        }
        
    }
}
