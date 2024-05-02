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

    private float traveledDistance;
    private AudioSource audioSource;
    private Rigidbody rb;
    void Start()
    {
        traveledDistance = 0f;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        traveledDistance += Time.deltaTime * speed;
        if (traveledDistance > maxDistance) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon")) { return; }
        if(other.isTrigger) { return; }

        audioSource.PlayOneShot(bulletHit);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localScale = Vector3.zero;
        
        Destroy(this.gameObject, bulletHit.length);
    }
}
