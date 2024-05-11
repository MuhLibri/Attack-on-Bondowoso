using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetDamageMovement : MonoBehaviour
{   
    public float stopRadius = 8;
    public float runRadius = 10;
    public GameObject owner;
    GameObject player;
    Rigidbody rbPet;
    NavMeshAgent agent;
    Vector3 destinationVar;

    void Start()
    {
        rbPet = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        destinationVar = transform.position;
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(owner == null) {
            PetHealth petHealth = GetComponent<PetHealth>();
            petHealth.TakeDamage(petHealth.currentHealth);
        }

        Chase();
        Run();

        agent.destination = destinationVar;
    }

    void Chase() {
        float distanceToOwner = Vector3.Distance(transform.position, owner.transform.position);
        if(distanceToOwner >= stopRadius) {
            destinationVar = owner.transform.position;
        } else if(distanceToOwner <= runRadius) {
            destinationVar = transform.position;
        }
    }

    void Run() {
        if(owner.tag != "Player" && player != null) {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if(distanceToPlayer <= runRadius) {
                // Debug.Log("Pet Damage Running");
                Vector3 directionRun = (transform.position - player.transform.position).normalized;
                destinationVar = transform.position + directionRun * 10;
            }
        }
    }
}
