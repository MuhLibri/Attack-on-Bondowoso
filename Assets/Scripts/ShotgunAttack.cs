using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    public int ammo;
    public GameObject ammoPrefab;
    public GameObject muzzlePrefab;
    public Transform shotPoint;
    public Transform muzzlePoint;
    public float shotForce;
    public float recoilForce;
    public float recoveryTime;
    public float cooldownTime;

    int maxAmmoShell = 5;
    Queue<GameObject> ammoShells = new Queue<GameObject>();
    float lastShotTime;
    Vector3 originalShotgunPosition;
    Quaternion originalShotgunRotation;
    bool isRecoiling = false;
    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -cooldownTime;
        originalShotgunPosition = transform.localPosition;
        originalShotgunRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShotTime >= cooldownTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        if (isRecoiling)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalShotgunPosition, Time.deltaTime / recoveryTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalShotgunRotation, Time.deltaTime / recoveryTime);
            if (Vector3.Distance(transform.localPosition, originalShotgunPosition) < 0.001f)
            {
                isRecoiling = false;
                transform.localPosition = originalShotgunPosition;
                transform.localRotation = originalShotgunRotation;
            }
        }
        
    }

    void Shoot()
    {
        GameObject flash = Instantiate(muzzlePrefab, muzzlePoint.position, muzzlePoint.rotation);
        flash.transform.SetParent(transform);

        GameObject ammo;
        if (ammoShells.Count >= maxAmmoShell)
        {
            ammo = ammoShells.Dequeue();
            ammo.transform.localPosition = shotPoint.position;
            ammo.transform.rotation = shotPoint.rotation;
        }
        else {
            ammo = Instantiate(ammoPrefab, shotPoint.position, shotPoint.rotation);
        }

        Rigidbody rb = ammo.GetComponent<Rigidbody>();
        if(ammoShells.Count >= maxAmmoShell)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("max");
        }
        ammoShells.Enqueue(ammo);

        rb.AddForce(shotPoint.forward * shotForce, ForceMode.Impulse);
        lastShotTime = Time.time;

        Recoil();
    }

    void Recoil()
    {
        isRecoiling = true;

        transform.Translate(-Vector3.forward * recoilForce);
        transform.Rotate(Vector3.left * 20);
    }
}
