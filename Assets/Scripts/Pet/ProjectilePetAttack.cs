using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePetAttack : MonoBehaviour
{
    public int damage = 10;
    public string targetTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }

        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
