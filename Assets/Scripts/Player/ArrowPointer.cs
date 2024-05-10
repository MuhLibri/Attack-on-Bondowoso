using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Transform playerTransform;
    private Transform closestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - playerTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
    }

    public void ToggleArrow(bool toggle)
    {
        GetComponentInChildren<MeshRenderer>().enabled = toggle;
    }
}
