using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float visionRadius = 10f;
    public float attackRadius = 2f;
    public float patrolRadius = 20f;
    public float patrolDistanceLimit = 5f;


    float patrolDistance;
    bool chasing;
    Transform player;
    Rigidbody rbEnemy;
    NavMeshAgent agent;
    Vector3 destinationVar;
    Vector3 lastPatrolPosition;

    void Start()
    {
        
    }

    void Awake() {
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPatrolPosition = transform.position;
        patrolDistance = 0f;
        
        rbEnemy.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void FixedUpdate() {

        ChaseOrNot();

        if(chasing) {
            Chase();
            patrolDistance = 0f;
        } else {
            patrolDistance += Vector3.Distance(transform.position, lastPatrolPosition);
            lastPatrolPosition = transform.position;

            if(patrolDistance >= patrolDistanceLimit){
                patrolDistance = 0f;
                Patrol();
            }
            
            if(transform.position == destinationVar) {
                Patrol();
            }
        }

        UpdateDestination();
    }

    void UpdateDestination() {
        agent.destination = destinationVar;
    }

    void ChaseOrNot(){
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer <= visionRadius) {
            chasing = true;
        } else {
            chasing = false;
        }
    }

    void Patrol() {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * patrolRadius * patrolDistanceLimit;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, patrolRadius, 1);
        Vector3 finalPos = hit.position;
        Debug.Log(finalPos);

        destinationVar = finalPos;
    }

    void Chase() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer >= attackRadius) {
            // Debug.Log("Chasing Player");
            destinationVar = player.position;
        } else {
            // Debug.Log("Stand Still");
            destinationVar = transform.position;
        }
    }
    
}
