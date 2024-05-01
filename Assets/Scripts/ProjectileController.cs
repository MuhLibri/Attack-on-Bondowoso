using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float maxDistance = 400f;
    public float damage = 10f;
    public float speed = 100f;

    private float traveledDistance;

    void Start()
    {
        traveledDistance = 0f;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        traveledDistance += Time.deltaTime * speed;
        if (traveledDistance > maxDistance) Destroy(this.gameObject);
    }
}
