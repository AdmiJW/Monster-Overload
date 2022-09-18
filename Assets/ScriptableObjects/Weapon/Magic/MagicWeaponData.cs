
using UnityEngine;


[CreateAssetMenu(fileName = "Magic", menuName = "Magic Weapon Data", order = 1)]
public class MagicWeaponData: WeaponData {
    
    [Header("Magic")]
    public float spellDamage;
    public float spellKnockback;
    public int targetNumber;
}

