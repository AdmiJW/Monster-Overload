using UnityEngine;


[System.Serializable]
public abstract class WeaponData : ScriptableObject {
    [Header("General Weapon")]
    public WeaponType weaponType;
    public int weaponLevel;
    public float attackCooldown;
}
