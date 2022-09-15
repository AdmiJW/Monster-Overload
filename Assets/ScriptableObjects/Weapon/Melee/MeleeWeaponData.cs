
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Melee Weapon Data", order = 1)]
public class MeleeWeaponData: WeaponData {
    
    [Header("Melee")]
    public float attackDamage;
    public float attackKnockback;
    public int maxEnemiesToAttack;
}
