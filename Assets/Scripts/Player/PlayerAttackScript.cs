using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// ! Since we are binding attack action and interact action to the same button,
// ! We have to perform check first: If interactable, no attacking should be done.

public class PlayerAttackScript : MonoBehaviour {
    [Header("Input References")]
    public InputActionReference attackInteractAction;

    private GameObject weaponParent;
    private List<GameObject> weapons = new List<GameObject>();
    private GameObject activeWeapon = null;

    private PlayerInteractScript playerInteractScript;
    private static ContactFilter2D ENEMY_CONTACT_FILTER = new ContactFilter2D();



    //===========================
    //  Lifecycle
    //===========================
    void Awake() {
        playerInteractScript = GetComponent<PlayerInteractScript>();
        
        ENEMY_CONTACT_FILTER.SetLayerMask(LayerMask.GetMask("Enemies"));
        weaponParent = transform.Find("WeaponParent").gameObject;
        
        ReloadAvailableWeapons();
    }

    void OnEnable() {
        attackInteractAction.action.performed += OnAttackInteractPerformed;
    }

    void Start() {
        foreach (GameObject weapon in weapons) weapon.SetActive(false);
        SetActiveWeapon(0);
    }

    void OnDisable() {
        attackInteractAction.action.performed -= OnAttackInteractPerformed;
    }


    //===========================
    //  Public
    //===========================
    public void ReloadAvailableWeapons() {
        weapons.Clear();
        foreach (Transform child in weaponParent.transform) {
            weapons.Add(child.gameObject);
            child.GetComponent<IWeapon>().Initialize(gameObject, ENEMY_CONTACT_FILTER);
        }
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
        activeWeapon.GetComponent<IWeapon>().TriggerAttack();
    }


    // Called from the animation event
    void DealDamage() {
        if (activeWeapon == null) return;
        activeWeapon.GetComponent<IWeapon>().DealDamage();
    }
}
