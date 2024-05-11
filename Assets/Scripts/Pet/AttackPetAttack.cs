using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPetAttack : MonoBehaviour
{
    public GameObject projectile;
    public float throwForce = 10f;
    public float throwUpForce = 0;
    public Transform attackPoint;

    public float coolDown = 1f;
    public GameObject target = null;
    public string targetTag = "Enemy";
    private List<GameObject> enemies = new List<GameObject>();
    private SphereCollider sphereCollider;
    private bool isAttacking;
    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        isAttacking = false;
        lastAttackTime = -coolDown;

    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveEnemy();
        CheckTargetStillValid();
        SetTarget();

        if (isAttacking)
        {
            Attack();
        }
    }

    void CheckActiveEnemy()
    {
        if (enemies.Count != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    return;
                }
            }
        }

    }

    void CheckTargetStillValid()
    {
        if (!CheckTargetInList(target))
        {
            target = null;
        }
    }

    void SetTarget()
    {
        if (enemies.Count > 0)
        {
            target = enemies[0];
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == targetTag && !CheckTargetInList(other.gameObject))
        {
            enemies.Add(other.gameObject);
            isAttacking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == targetTag && CheckTargetInList(other.gameObject))
        {
            enemies.Remove(other.gameObject);
            // Debug.Log("Enemy removed");
            if (enemies.Count == 0)
            {
                isAttacking = false;
            }
        }

    }


    bool CheckTargetInList(GameObject target)
    {
        if (enemies.Count == 0)
        {
            return false;
        }

        foreach (GameObject enemy in enemies)
        {
            if (enemy == target)
            {
                return true;
            }
        }
        return false;
    }

    void Attack()
    {
        if (isAttacking && target != null && Time.time >= lastAttackTime + coolDown)
        {
            GameObject newProjectile = Instantiate(projectile, attackPoint.position, attackPoint.rotation);

            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            Vector3 throwDirection = (target.transform.position - attackPoint.position).normalized;

            Vector3 forceDirection = throwDirection * throwForce + Vector3.up * throwUpForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);

            lastAttackTime = Time.time;
        }
    }
}
