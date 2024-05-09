using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float visionRadius = 10f;
    public float attackRadius = 2f;
    public float patrolRadius = 20f;
    public float patrolDistanceLimit = 5f;
    public float rotationSpeed = 5f;
    public float attackCooldownTime = 3f;
    public Animator enemyAnimator;
    public List<AudioClip> audioClips;

    private float lastAttackTime;
    private Transform player;
    private NavMeshAgent agent;
    private Vector3 lastPatrolPosition;
    private AudioSource audioSource;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        lastPatrolPosition = transform.position;
        agent.updateRotation = false;
        lastAttackTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if ((GetComponent<EnemyHealth>().currentHealth <= 0) || (player == null))
        {
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("isChasing", false);
            enemyAnimator.SetBool("isPatrolling", false);
            agent.ResetPath();
        }
        else if (IsAttacking())
        {
            Attack();
        }
        else if (IsChasing())
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    bool IsAttacking()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= attackRadius;
    }

    bool IsChasing()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRadius && distanceToPlayer > attackRadius;
    }

    void Attack()
    {
        // Set animation bools
        enemyAnimator.SetBool("isAttacking", true);
        enemyAnimator.SetBool("isChasing", false);
        enemyAnimator.SetBool("isPatrolling", false);
        // Rotate towards the player
        RotateTowards(player.position);
        // Reset agent path
        agent.ResetPath();
        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime >= attackCooldownTime)
        {
            enemyAnimator.SetBool("isAttacking", false);
            audioSource.PlayOneShot(audioClips[0]);
            lastAttackTime = Time.time;
        }
    }

    void Chase()
    {
        // Set animation bools
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isChasing", true);
        enemyAnimator.SetBool("isPatrolling", false);
        // Continue chasing the player
        agent.SetDestination(player.position);
        // Rotate towards the player
        RotateTowards(player.position);
    }

    void Patrol()
    {
        // Set animation bools
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isChasing", false);
        enemyAnimator.SetBool("isPatrolling", true);
        if (Vector3.Distance(transform.position, lastPatrolPosition) >= patrolDistanceLimit)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * patrolRadius;
            Vector3 randomPos = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, patrolRadius, 1))
            {
                lastPatrolPosition = hit.position;
                agent.SetDestination(lastPatrolPosition);
            }
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
