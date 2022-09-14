
using UnityEngine;

// Initialize weapon from weaponData and return the instantiated prefab
public class WeaponLoader : AbstractManager<WeaponLoader> {
    
    [Header("Prefabs")]
    public GameObject punchPrefab;


    public GameObject getWeapon(WeaponData data) {
        switch (data.name) {
            case WeaponType.PUNCH:
                GameObject punch = Instantiate(punchPrefab);
                punch.GetComponent<Punch>().LoadFromWeaponData( (MeleeWeaponData)data );
                return punch;
            default:
                return null;
        }
    }

}
