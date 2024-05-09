using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHeal : MonoBehaviour
{
    public float healPercentage = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            int healAmount = (int)((healPercentage / 100) * playerHealth.currentHealth);
            playerHealth.Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
