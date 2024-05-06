using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float damage = 10f;
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
        Debug.Log("Kena " + other.tag + ": " + other.CompareTag("Enemy"));
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Musuh");
            // Assuming you've set damage on your projectiles
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("Masuk");
                enemyHealth.TakeDamage(damage);
                // Destroy(other.gameObject);
            }
        }
    }
}
