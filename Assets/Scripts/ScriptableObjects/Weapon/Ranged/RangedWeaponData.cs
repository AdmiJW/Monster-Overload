using UnityEngine;


[CreateAssetMenu(fileName = "Ranged", menuName = "Ranged Weapon Data", order = 2)]
public class RangedWeaponData : WeaponData {
    public float projectileSpeed;
    public float projectileAttack;
    public float projectileKnockback;
    public bool isPiercing;
}
