using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpeed : MonoBehaviour
{
    public int speedPercentage = 100;
    public float duration = 15f;

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
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            playerMovement.startSpeedIncrease(duration, speedPercentage);
            Destroy(this.gameObject);
        }
    }
}
