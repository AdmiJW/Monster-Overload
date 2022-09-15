
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Melee Weapon Data", order = 1)]
public class MeleeWeaponData: WeaponData {
    public float attackDamage;
    public float attackKnockback;
    public int maxEnemiesToAttack;
}
