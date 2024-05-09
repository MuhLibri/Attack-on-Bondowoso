using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponState : MonoBehaviour
{ 
    /* for documentation purposes:
        0 = sword
        1 = shotgun
    
    */

    public enum WeaponType {
        None = -1,
        MachinePistol = 0,
        Shotgun = 1,
        Sword = 2
    }

    Animator playerAnimator;
    GameObject[] weapons;
    bool aiming;
    WeaponType currentWeapon;
    WeaponType? previousWeapon;
    GameObject currentWeaponObject;
    PlayerMovement playerMovement;
    GameObject roroModel;
    OnAttack onAttack;
    int damageOrbCount;
    public int orbCountMax = 15;



    // Start is called before the first frame update
    void Start()
    {
        damageOrbCount = 0;
        roroModel = GameObject.FindGameObjectWithTag("RoroModel");
        onAttack = roroModel.GetComponent<OnAttack>();
        previousWeapon = null;
        currentWeapon = WeaponType.None;
        playerAnimator = GetComponentInChildren<Animator>();
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        aiming = false;
        playerMovement = GetComponent<PlayerMovement>();

        WeaponStateUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        AimingCheck();
        WeaponChangeInput();
        WeaponStateUpdate();

        ResrictMovement();
    }

    GameObject GetWeaponWithName(string weaponName){
        foreach(GameObject weapon in weapons){
            if(weapon.name == weaponName){
                return weapon;
            }
        }

        return null;
    }

    void WeaponChangeInput(){
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        int scrollSign = (int) Mathf.Sign(scrollInput);
        int weaponValue = (int) currentWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            weaponValue = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            weaponValue = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            weaponValue = 2;
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            weaponValue = -1;
        }
        else if(scrollInput != 0) {
            weaponValue += scrollSign;
            weaponValue = weaponValue % weapons.Length;
        }  

        previousWeapon = currentWeapon;
        currentWeapon = (WeaponType) weaponValue;
        currentWeaponObject = GetWeaponWithName(currentWeapon.ToString());
    }

    void WeaponStateUpdate(){
        if(currentWeapon != previousWeapon){
            if(currentWeapon != WeaponType.None){
                ActivateWeapon();
                DeactivateOtherWeapons();
                
                if (currentWeapon == WeaponType.Sword){
                    playerAnimator.SetLayerWeight(1, 0f);
                    playerAnimator.SetBool("Equip Sword", true);
                }
                else if(currentWeapon == WeaponType.Shotgun){
                    playerAnimator.SetLayerWeight(1, 1f);
                    playerAnimator.SetBool("Equip Sword", false);
                }
                else if(currentWeapon == WeaponType.MachinePistol){
                    playerAnimator.SetLayerWeight(1, 0f);
                    playerAnimator.SetBool("Equip Sword", false);
                }
            }

            else { // if no weapon is selected
                foreach(GameObject weapon in weapons){
                    weapon.SetActive(false);
                }

                playerAnimator.SetLayerWeight(1, 0f);
                playerAnimator.SetBool("Equip Sword", false);
            }
        }
    }

    void DeactivateOtherWeapons(){
        foreach(GameObject weapon in weapons){
            if(weapon != currentWeaponObject){
                weapon.SetActive(false);
            }
        }
    }

    void ActivateWeapon(){
        currentWeaponObject.SetActive(true);
        
        if(currentWeapon == WeaponType.Sword){
            playerAnimator.SetLayerWeight(1, 0f);
        }
        else if(currentWeapon == WeaponType.Shotgun){
            playerAnimator.SetLayerWeight(1, 1f);
        }
    }

    void AimingCheck() {

        bool aimingInput = Input.GetButton("Fire2");
        playerAnimator.SetBool("Aim", aiming);

        if (aimingInput && (currentWeapon == WeaponType.Shotgun || currentWeapon == WeaponType.MachinePistol))
        {   
            aiming = aimingInput;
            playerAnimator.SetLayerWeight(2, 1f);
        }
        else
        {   
            aiming = aimingInput;
            playerMovement.moveAllowed = true;
            playerAnimator.SetLayerWeight(2, 0f);
        }
        
    }

    void ResrictMovement(){
        if(aiming || onAttack.attacking){
            playerMovement.moveAllowed = false;
        }
        else{
            playerMovement.moveAllowed = true;
        }
    }

    public void DamageBoost(int percentage) {
        if(damageOrbCount <= orbCountMax) {
            damageOrbCount += 1;
            if(currentWeapon == WeaponType.Sword) {
                SwordScript swordScript = currentWeaponObject.GetComponent<SwordScript>();
                swordScript.damage += swordScript.damage * percentage / 100;
                Debug.Log("Damage Boosted to: " + swordScript.damage.ToString("F2"));
            }
        }
    }
}
