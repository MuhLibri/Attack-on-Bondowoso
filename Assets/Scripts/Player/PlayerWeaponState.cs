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
    List<GameObject> weapons;
    Transform[] allChildren;
    bool aiming;
    WeaponType currentWeapon;
    WeaponType? previousWeapon;
    GameObject currentWeaponObject;
    PlayerMovement playerMovement;
    GameObject roroModel;
    OnAttack onAttack;
    int damageOrbCount;
    public int orbCountMax = 15;
    public int petDamageCount;
    public int petDamageBoostPercentage;



    // Start is called before the first frame update
    void Start()
    {
        petDamageCount = 0;
        petDamageBoostPercentage = 0;
        damageOrbCount = 0;
        roroModel = GameObject.FindGameObjectWithTag("RoroModel");
        onAttack = roroModel.GetComponent<OnAttack>();
        previousWeapon = null;
        currentWeapon = WeaponType.None;
        playerAnimator = GetComponentInChildren<Animator>();
        aiming = false;
        playerMovement = GetComponent<PlayerMovement>();

        weapons = new List<GameObject>();
        allChildren = transform.GetComponentsInChildren<Transform>();
        foreach(Transform child in allChildren){
            if(child.tag == "Weapon"){
                weapons.Add(child.gameObject);
            }
        }

        WeaponStateUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        AimingCheck();
        WeaponChangeInput();
        WeaponStateUpdate();

        ResrictMovement();

        petDamageBoost();
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
            weaponValue = weaponValue % weapons.Count;
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

            foreach(GameObject weapon in weapons){
                if(weapon.name == "MachinePistol"){
                    MachinePistolAttack machinePistolAttack = weapon.GetComponent<MachinePistolAttack>();
                    int damageBoost = machinePistolAttack.defaultDamage * percentage / 100;

                    machinePistolAttack.damage = machinePistolAttack.damage + damageBoost; 
                    machinePistolAttack.defaultDamage = machinePistolAttack.defaultDamage + damageBoost;
                }
                else if(weapon.name == "Shotgun"){
                    ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
                    int damageBoost = shotgunAttack.defaultDamage * percentage / 100;

                    shotgunAttack.damage = shotgunAttack.damage + damageBoost; 
                    shotgunAttack.defaultDamage = shotgunAttack.defaultDamage + damageBoost;
                }
                else if(weapon.name == "Sword"){
                    SwordScript swordAttack = weapon.GetComponent<SwordScript>();
                    int damageBoost = swordAttack.defaultDamage * percentage / 100;

                    swordAttack.damage = swordAttack.damage + damageBoost; 
                    swordAttack.defaultDamage = swordAttack.defaultDamage + damageBoost;
                }
            }
        }
    }

    void setDefaultDamage(){
        foreach(GameObject weapon in weapons){
            if(weapon.name == "MachinePistol"){
                MachinePistolAttack machinePistolAttack = weapon.GetComponent<MachinePistolAttack>();
                machinePistolAttack.damage = machinePistolAttack.defaultDamage;
            }
            else if(weapon.name == "Shotgun"){
                ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
                shotgunAttack.damage = shotgunAttack.defaultDamage;
            }
            else if(weapon.name == "Sword"){
                SwordScript swordAttack = weapon.GetComponent<SwordScript>();
                swordAttack.damage = swordAttack.defaultDamage;
            }
        }
    }

    void addDamage(int percentage, int multiplier){
        foreach(GameObject weapon in weapons){
            if(weapon.name == "MachinePistol"){
                MachinePistolAttack machinePistolAttack = weapon.GetComponent<MachinePistolAttack>();
                int damageBoost = machinePistolAttack.defaultDamage * percentage / 100;

                machinePistolAttack.damage = machinePistolAttack.damage + (damageBoost * multiplier); 
            }
            else if(weapon.name == "Shotgun"){
                ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
                int damageBoost = shotgunAttack.defaultDamage * percentage / 100;

                shotgunAttack.damage = shotgunAttack.damage + damageBoost+ (damageBoost * multiplier); 
            }
            else if(weapon.name == "Sword"){
                SwordScript swordAttack = weapon.GetComponent<SwordScript>();
                int damageBoost = swordAttack.defaultDamage * percentage / 100;

                swordAttack.damage = swordAttack.damage + damageBoost+ (damageBoost * multiplier); 
            }
        }
    }

    void decreaseDamage(int percentage, int multiplier){
        foreach(GameObject weapon in weapons){
            if(weapon.name == "MachinePistol"){
                MachinePistolAttack machinePistolAttack = weapon.GetComponent<MachinePistolAttack>();
                int damageBoost = machinePistolAttack.defaultDamage * percentage / 100;

                machinePistolAttack.damage = machinePistolAttack.damage - (damageBoost * multiplier); 
            }
            else if(weapon.name == "Shotgun"){
                ShotgunAttack shotgunAttack = weapon.GetComponent<ShotgunAttack>();
                int damageBoost = shotgunAttack.defaultDamage * percentage / 100;

                shotgunAttack.damage = shotgunAttack.damage - damageBoost+ (damageBoost * multiplier); 
            }
            else if(weapon.name == "Sword"){
                SwordScript swordAttack = weapon.GetComponent<SwordScript>();
                int damageBoost = swordAttack.defaultDamage * percentage / 100;

                swordAttack.damage = swordAttack.damage - damageBoost+ (damageBoost * multiplier); 
            }
        }
    }


    public void petDamageBoost(){
        if (petDamageCount == 0) {
            setDefaultDamage();
        }
        else {
            addDamage(petDamageBoostPercentage, petDamageCount);
        }
    }
}
