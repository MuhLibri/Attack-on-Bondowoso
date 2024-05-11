using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetAttackMovement : MonoBehaviour
{
    public float stopRadius = 8f;
    public float sprintSpeed;

    GameObject targetEnemy;
    AttackPetAttack attackPetAttack;

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
        attackPetAttack = GetComponentInChildren<AttackPetAttack>();
        targetEnemy = attackPetAttack.target;
    }

    // Update is called once per frame
    void Update()
    {   
        if(owner == null) {
            PetHealth petHealth = GetComponent<PetHealth>();
            petHealth.TakeDamage(petHealth.currentHealth);
        }

        targetEnemy = attackPetAttack.target;
        if(targetEnemy != null) {
            Attacking();
        } else {
            Chase();
        }
        
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

    void Attacking (){
        float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.transform.position);

        if(distanceToTarget >= stopRadius) {
            destinationVar = targetEnemy.transform.position;
            petAnimator.SetBool("Moving", true);
        } else {
            petAnimator.SetBool("Moving", false);
            destinationVar = transform.position;
        }

    }
}
