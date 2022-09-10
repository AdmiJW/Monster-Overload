using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponScript : MonoBehaviour
{
    [Header("Input References")]
    public InputActionReference attackAction;

    GameObject weaponParent;
    List<GameObject> weapons = new List<GameObject>();
    GameObject activeWeapon = null;

    private static ContactFilter2D ENEMY_CONTACT_FILTER = new ContactFilter2D();



    //===========================
    //  Lifecycle
    //===========================
    void Awake() {
        ENEMY_CONTACT_FILTER.SetLayerMask(LayerMask.GetMask("Enemies"));
        weaponParent = transform.Find("WeaponParent").gameObject;
        
        ReloadAvailableWeapons();
        attackAction.action.performed += OnAttackPerformed;
    }

    void Start() {
        foreach (GameObject weapon in weapons) weapon.SetActive(false);
        SetActiveWeapon(0);
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
    void OnAttackPerformed(InputAction.CallbackContext ctx) {
        if (activeWeapon == null) return;
        activeWeapon.GetComponent<IWeapon>().TriggerAttack();
    }


    // Called from the animation event
    void DealDamage() {
        if (activeWeapon == null) return;
        activeWeapon.GetComponent<IWeapon>().DealDamage();
    }
}
