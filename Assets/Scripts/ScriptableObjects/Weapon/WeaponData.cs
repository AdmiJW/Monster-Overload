using UnityEngine;


[System.Serializable]
public abstract class WeaponData : ScriptableObject {
    public WeaponType weaponType;
    public float attackCooldown;
}
