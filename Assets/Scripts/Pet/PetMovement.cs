using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    public float stopRadius = 5f;
    public float sprintSpeed;

    public GameObject owner;
    Rigidbody rbPet;
    NavMeshAgent agent;
    Vector3 destinationVar;
    Animator petAnimator;


    // Start is called before the first frame update
    void Start()
    {
        rbPet = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        destinationVar = transform.position;
        petAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        agent.destination = destinationVar;
        if (Input.GetKey(KeyCode.LeftShift)) {
            agent.speed = sprintSpeed;
        }
    }

    void Chase() {
        float distanceToPlayer = Vector3.Distance(transform.position, owner.transform.position);

        if(distanceToPlayer >= stopRadius) {
            destinationVar = owner.transform.position;
            petAnimator.SetBool("Moving", true);
        } else {
            petAnimator.SetBool("Moving", false);
            destinationVar = transform.position;
        }
    }
}
