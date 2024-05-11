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
    private Transform saveZone;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    void Awake()
    {
        saveZone = GameObject.FindGameObjectWithTag("SaveZone")?.transform;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        lastAttackTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!IsInsideVillage())
        {
            if ((GetComponent<EnemyHealth>().currentHealth <= 0) || (player == null))
            {
                enemyAnimator.SetBool("isAttacking", false);
                enemyAnimator.SetBool("isChasing", false);
                enemyAnimator.SetBool("isPatrolling", false);
                agent.ResetPath();
            }
            else if (IsUsingShotgun() && IsTooCloseToPlayer())
            {
                RunAway();
            }
            else if (IsAttacking() && !IsRunningAway())
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
        else
        {
            GetAwayFromVillage();
        }
    }

    public bool IsAttacking()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= attackRadius;
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
            if (audioClips.Count > 0)
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
            lastAttackTime = Time.time;
        }
    }

    bool IsChasing()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRadius && distanceToPlayer > attackRadius;
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

        // Check if the enemy has reached the current patrol destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Generate a new random patrol destination
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            // Ensure the new destination is on the NavMesh
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, 1))
            {
                RotateTowards(hit.position);
                agent.SetDestination(hit.position);
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

    bool IsUsingShotgun()
    {
        return GetComponentInChildren<ShotgunAttack>() != null;
    }

    bool IsTooCloseToPlayer()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= attackRadius * 0.5f;
    }

    bool IsInsideVillage()
    {
        if (saveZone == null) return false;
        return Vector3.Distance(transform.position, saveZone.position) <= 125;
    }

    void GetAwayFromVillage()
    {
        Vector3 runDirection = transform.position - saveZone.position;
        runDirection.y = 0;
        runDirection.Normalize();
        Vector3 runDestination = transform.position + runDirection * patrolRadius;
        if (NavMesh.SamplePosition(runDestination, out NavMeshHit hit, patrolRadius, 1))
        {
            agent.SetDestination(hit.position);
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("isChasing", false);
            enemyAnimator.SetBool("isPatrolling", true);
        }
    }

    public bool IsRunningAway()
    {
        return (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Walk") || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Male_Sword_Walk") || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Sprint") || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Male Sword Sprint"));
    }

    void RunAway()
    {
        // Calculate the direction away from the player
        Vector3 runDirection = transform.position - player.position;
        runDirection.y = 0; // Ensure no vertical movement

        // Normalize the direction to get a unit vector
        runDirection.Normalize();

        // Calculate the destination point where the enemy should run to
        Vector3 runDestination = transform.position + runDirection * patrolRadius;

        // Check if the destination is valid on the NavMesh
        if (NavMesh.SamplePosition(runDestination, out NavMeshHit hit, patrolRadius, 1))
        {
            // Set the destination for the NavMeshAgent to run away
            agent.SetDestination(hit.position);

            // Set animation bools
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("isChasing", false);
            enemyAnimator.SetBool("isPatrolling", true);
        }
    }
}
