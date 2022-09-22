using UnityEngine;
using NaughtyAttributes;


public abstract class AbstractRangedWeapon : AbstractWeapon<RangedWeaponData> {

    [BoxGroup("References")]
    public GameObject projectilePrefab;

    //==============================
    // Abstract method
    //==============================
    public abstract Projectile GetProjectile();
    
    //===========================
    // Logic
    //===========================
    public override void Attack() {
        GetProjectile().Shoot();
    }
}
