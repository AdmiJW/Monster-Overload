using UnityEngine;
using NaughtyAttributes;


public abstract class AbstractRangedWeapon<T> : AbstractWeapon<T> where T: RangedWeaponData {

    [BoxGroup("References")]
    public GameObject projectilePrefab;

    //==============================
    // Abstract method
    //==============================
    public abstract Projectile<T> GetProjectile();
    
    //===========================
    // Logic
    //===========================
    public override void Attack() {
        GetProjectile().Shoot();
    }
}
