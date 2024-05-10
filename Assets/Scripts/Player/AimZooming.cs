using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimZooming : MonoBehaviour
{
    public GameObject player;
    public float verticalFOV = 70f;
    public float fovChangeSpeed = 5f;
    public float zoomVerticalFOV = 40f;
    CinemachineVirtualCamera vcam;
    PlayerWeaponState playerWeaponState;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        playerWeaponState = player.GetComponent<PlayerWeaponState>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetFOV = playerWeaponState.aiming ? zoomVerticalFOV : verticalFOV;
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, targetFOV, fovChangeSpeed * Time.deltaTime);
    }
    
}
