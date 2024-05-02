using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float xSensitivity = 100;
    public float ySensitivity = 100;

    float xRotate;
    float yRotate;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.fixedDeltaTime;

        // xRotate -= mouseY;
        // xRotate = Mathf.Clamp(xRotate, -90f, 90f);
        // commented because its rotating all the player componentsup or down
        // it should be just the gun

        yRotate += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotate, 0);

    }
}
