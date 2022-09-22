using UnityEngine;


public abstract class AbstractRangedWeapon : AbstractWeapon<RangedWeaponData> {

    [Header("References")]
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
