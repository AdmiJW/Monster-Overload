using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// ! Since we are binding attack action and interact action to the same button,
// ! We have to perform check first: If interactable, no attacking should be done.

public class PlayerAttackScript : MonoBehaviour {
    [Header("Reference")]
    public GameObject weaponParent;

    [Header("Input References")]
    public InputActionReference attackInteractAction;
    public InputActionReference switchToPunchAction;
    public InputActionReference switchToSwordAction;

    //===========================
    public WeaponType activeWeaponType { get; private set; }
    public GameObject activeWeapon { get; private set; }
    public Dictionary<WeaponType, GameObject> weapons { get; private set; }

    private PlayerInteractScript playerInteractScript;



    //===========================
    //  Lifecycle
    //===========================
    void Awake() {
        playerInteractScript = GetComponent<PlayerInteractScript>();

        // Initialize Dictionary with null
        weapons = new Dictionary<WeaponType, GameObject>();
        foreach (WeaponType type in Enum.GetValues( typeof(WeaponType) ) )
            weapons.Add(type, null);

        // Load user's weapons from persistent PlayerManager
        if (PlayerManager.instance.playerWeapons != null) {
            PlayerManager.instance.playerWeapons.ForEach(weaponData => {
                GameObject weapon = WeaponLoader.instance.getWeapon(weaponData);
                weapons[weaponData.weaponType] = weapon;
                weapon.transform.parent = weaponParent.transform;
            });
        }
    }

    void OnEnable() {
        attackInteractAction.action.performed += OnAttackInteractPerformed;
        switchToPunchAction.action.performed += SwitchToPunch;
        switchToSwordAction.action.performed += SwitchToSword;

    }

    void Start() {
        foreach( GameObject weapon in weapons.Values )
            if (weapon != null) weapon.SetActive(false);

        SwitchActiveWeapon(PlayerManager.instance.activeWeaponType);
    }

    void OnDisable() {
        attackInteractAction.action.performed -= OnAttackInteractPerformed;
        switchToPunchAction.action.performed -= SwitchToPunch;
        switchToSwordAction.action.performed -= SwitchToSword;
    }


    //===========================
    //  Public
    //===========================

    // Adds a weapon prefab into the player's weapon system. If the same weapon type already exists, it will be replaced.
    public void SetWeapon(GameObject weapon) {
        IWeapon weaponScript = weapon.GetComponent<IWeapon>();
        if (weaponScript == null) throw new ArgumentException("PlayerAttackScript: Weapon does not have IWeapon script");

        WeaponType weaponType = weaponScript.GetWeaponData().weaponType;
        if (weapons[weaponType] != null) Destroy( weapons[weaponType] );
        weapons[weaponType] = weapon;
    }



    public void SwitchActiveWeapon(WeaponType type) {
        if (weapons[type] == null) return;

        if (activeWeapon != null) activeWeapon.SetActive(false);
        activeWeaponType = type;
        activeWeapon = weapons[type];
        activeWeapon.SetActive(true);
    }


    

    //===========================
    //  Handler
    //==========================
    void OnAttackInteractPerformed(InputAction.CallbackContext ctx) {
        // Only perform attack if there is no interactable in range
        if ( playerInteractScript.Interact() ) return;
        if ( activeWeapon == null ) return;

        activeWeapon.GetComponent<IWeapon>().TriggerAttack();
    }


    // Called from the animation event
    void DealDamage() {
        if ( activeWeapon == null) return;
        activeWeapon.GetComponent<IWeapon>().DealDamage();
    }


    void SwitchToPunch(InputAction.CallbackContext ctx) {
        if (weapons[WeaponType.PUNCH] == null) return;
        SwitchActiveWeapon(WeaponType.PUNCH);
    }

    void SwitchToSword(InputAction.CallbackContext ctx) {
        if (weapons[WeaponType.SWORD] == null) return;
        SwitchActiveWeapon(WeaponType.SWORD);
    }
}
