using System;
using UnityEngine;

// Initialize weapon from weaponData and return the instantiated prefab
public class WeaponLoader : AbstractManager<WeaponLoader> {
    
    [Header("Prefabs")]
    public GameObject punchPrefab;
    public GameObject swordPrefab;
    public GameObject bowPrefab;


    public GameObject getWeapon(WeaponData data) {
        switch (data.weaponType) {
            case WeaponType.PUNCH:
                GameObject punch = Instantiate(punchPrefab);
                punch.GetComponent<Punch>().weaponData = (MeleeWeaponData)data;
                return punch;
            case WeaponType.SWORD:
                GameObject sword = Instantiate(swordPrefab);
                sword.GetComponent<Sword>().weaponData = (MeleeWeaponData)data;
                return sword;
            case WeaponType.BOW:
                GameObject bow = Instantiate(bowPrefab);
                bow.GetComponent<Bow>().weaponData = (RangedWeaponData)data;
                return bow;
            default:
                throw new NotImplementedException("WeaponLoader: Weapon type not found");
        }
    }

}
