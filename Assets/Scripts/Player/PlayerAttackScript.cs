using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// ! Since we are binding attack action and interact action to the same button,
// ! We have to perform check first: If interactable, no attacking should be done.

public class PlayerAttackScript : MonoBehaviour, IWeapon {
    [Header("Reference")]
    public GameObject weaponParent;

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
        weapons = new Dictionary<WeaponType, GameObject>();

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
        InputManager.instance.player.attackOrInteract.action.performed += OnAttackInteractPerformed;
        InputManager.instance.player.switchNext.action.performed += SwitchToNextWeapon;
        InputManager.instance.player.switchPrevious.action.performed += SwitchToPrevWeapon;
        InputManager.instance.player.switchPunch.action.performed += SwitchToPunch;
        InputManager.instance.player.switchSword.action.performed += SwitchToSword;
        InputManager.instance.player.switchBow.action.performed += SwitchToBow;
        InputManager.instance.player.switchFireStaff.action.performed += SwitchToFireStaff;
    }

    void Start() {
        foreach( GameObject weapon in weapons.Values ) weapon.SetActive(false);
        SwitchActiveWeapon(PlayerManager.instance.activeWeaponType);
    }

    void OnDisable() {
        InputManager.instance.player.attackOrInteract.action.performed -= OnAttackInteractPerformed;
        InputManager.instance.player.switchNext.action.performed -= SwitchToNextWeapon;
        InputManager.instance.player.switchPrevious.action.performed -= SwitchToPrevWeapon;
        InputManager.instance.player.switchPunch.action.performed -= SwitchToPunch;
        InputManager.instance.player.switchSword.action.performed -= SwitchToSword;
        InputManager.instance.player.switchBow.action.performed -= SwitchToBow;
        InputManager.instance.player.switchFireStaff.action.performed -= SwitchToFireStaff;
    }


    //===========================
    //  Public
    //===========================

    // Adds a weapon prefab into the player's weapon system. If the same weapon type already exists, it will be replaced.
    public void SetWeapon(GameObject weapon) {
        IWeapon weaponScript = weapon.GetComponent<IWeapon>();

        weapon.transform.parent = weaponParent.transform;

        WeaponType weaponType = weaponScript.GetWeaponData().weaponType;
        if (weapons.ContainsKey(weaponType)) Destroy( weapons[weaponType] );
        weapons[weaponType] = weapon;
    }



    public void SwitchActiveWeapon(WeaponType type, bool checkAlreadyEquip = false, bool notify = false) {
        if ( !weapons.ContainsKey(type) ) return;
        if (checkAlreadyEquip && type == activeWeaponType) return;

        if (activeWeapon != null) activeWeapon.SetActive(false);
        activeWeaponType = type;
        activeWeapon = weapons[type];
        activeWeapon.SetActive(true);

        if (!notify) return;
        PlayerManager.instance.ShowStatusText( type.Name() + " equipped" );
        PlayerAudioManager.instance.switchWeapon.Play();
    }


    

    //===========================
    //  Interface Methods
    //==========================
    public void OnAttackStart() {
        activeWeapon.GetComponent<IWeapon>().OnAttackStart();
    }

    public void OnAttackEnd() {
        activeWeapon.GetComponent<IWeapon>().OnAttackEnd();
    }

    public void OnAttackPerform() {
        activeWeapon.GetComponent<IWeapon>().OnAttackPerform();
    }

    public WeaponData GetWeaponData() {
        if (activeWeapon == null) return null;
        return activeWeapon.GetComponent<IWeapon>().GetWeaponData();
    }


    //===========================
    //  Handlers
    //===========================
    void OnAttackInteractPerformed(InputAction.CallbackContext ctx) {
        // Only perform attack if there is no interactions
        if ( playerInteractScript.Interact() ) return;
        if ( activeWeapon == null ) return;
        OnAttackStart();
    }


    void SwitchToNextWeapon(InputAction.CallbackContext ctx) {
        List<WeaponType> weaponTypeList = GetAvailableWeaponTypes();
        if (weaponTypeList.Count == 0) return;
        int index = ( weaponTypeList.IndexOf(activeWeaponType) + 1 ) % weaponTypeList.Count;
        SwitchActiveWeapon(weaponTypeList[index], true, true);
    }


    void SwitchToPrevWeapon(InputAction.CallbackContext ctx) {
        List<WeaponType> weaponTypeList = GetAvailableWeaponTypes();
        if (weaponTypeList.Count == 0) return;
        int index = weaponTypeList.IndexOf(activeWeaponType) - 1;
        if (index < 0) index = weaponTypeList.Count - 1;
        SwitchActiveWeapon(weaponTypeList[index], true, true);
    }


    void SwitchToPunch(InputAction.CallbackContext ctx) {
        SwitchActiveWeapon(WeaponType.PUNCH, true, true);
    }

    void SwitchToSword(InputAction.CallbackContext ctx) {
        SwitchActiveWeapon(WeaponType.SWORD, true, true);
    }

    void SwitchToBow(InputAction.CallbackContext ctx) {
        SwitchActiveWeapon(WeaponType.BOW, true, true);
    }

    void SwitchToFireStaff(InputAction.CallbackContext ctx) {
        SwitchActiveWeapon(WeaponType.FIRE_STAFF, true, true);
    }


    //===========================
    //  Helper
    //===========================
    List<WeaponType> GetAvailableWeaponTypes() {
        List<WeaponType> weaponTypes = new List<WeaponType>();
        foreach( GameObject weapon in weapons.Values ) {
            if (weapon == null || weapon.GetComponent<IWeapon>() == null) continue;
            weaponTypes.Add( weapon.GetComponent<IWeapon>().GetWeaponData().weaponType );
        }
        return weaponTypes;
    }

}
