using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnderling : MonoBehaviour
{
    public GameObject underling;
    public float spawnTime = 25f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = spawnTime;
        }        
    }

    void Spawn() {
        Instantiate(underling, transform.position + transform.forward * 3, transform.rotation);
    }
}
