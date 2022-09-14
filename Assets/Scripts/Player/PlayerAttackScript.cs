using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// ! Since we are binding attack action and interact action to the same button,
// ! We have to perform check first: If interactable, no attacking should be done.

public class PlayerAttackScript : MonoBehaviour {
    [Header("Input References")]
    public InputActionReference attackInteractAction;

    public GameObject weaponParent { get; private set; }
    public List<GameObject> weapons { get; private set; } = new List<GameObject>();
    public GameObject activeWeapon { get; private set; } = null;

    private PlayerInteractScript playerInteractScript;



    //===========================
    //  Lifecycle
    //===========================
    void Awake() {
        playerInteractScript = GetComponent<PlayerInteractScript>();
        
        weaponParent = transform.Find("WeaponParent").gameObject;

        // Load user's weapons from PlayerManager
        if (PlayerManager.instance.playerWeapons != null) {
            PlayerManager.instance.playerWeapons.ForEach(weaponData => {
                GameObject weapon = WeaponLoader.instance.getWeapon(weaponData);
                weapon.transform.parent = weaponParent.transform;
            });
        }

        ReloadAvailableWeapons();
    }

    void OnEnable() {
        attackInteractAction.action.performed += OnAttackInteractPerformed;
    }

    void Start() {
        foreach (GameObject weapon in weapons) weapon.SetActive(false);
        SetActiveWeapon( PlayerManager.instance.activeWeaponIndex );
    }

    void OnDisable() {
        attackInteractAction.action.performed -= OnAttackInteractPerformed;
    }


    //===========================
    //  Public
    //===========================
    public void ReloadAvailableWeapons() {
        weapons.Clear();
        foreach (Transform child in weaponParent.transform)
            weapons.Add(child.gameObject);
    }


    public void SetActiveWeapon(int index) {
        if (index < 0 || index >= weapons.Count) return;
        if (activeWeapon != null) activeWeapon.SetActive(false);
        activeWeapon = weapons[index];
        weapons[index].SetActive(true);
    }


    public void AddNewWeapon(GameObject weapon) {
        weapon.transform.SetParent(weaponParent.transform);
        ReloadAvailableWeapons();
    }



    //===========================
    //  Handler
    //==========================
    void OnAttackInteractPerformed(InputAction.CallbackContext ctx) {
        // Only perform attack if there is no interactable in range
        if ( playerInteractScript.Interact() ) return;

        if (activeWeapon == null) return;
        activeWeapon.GetComponent<AbstractWeapon>().TriggerAttack();
    }


    // Called from the animation event
    void DealDamage() {
        if (activeWeapon == null) return;
        activeWeapon.GetComponent<AbstractWeapon>().DealDamage();
    }
}
