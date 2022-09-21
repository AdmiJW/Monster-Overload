using UnityEngine;


public abstract class AbstractRangedWeapon : AbstractWeapon<RangedWeaponData> {

    [Header("References")]
    public GameObject projectilePrefab;


    // Method to obtain a working projectile, must contain script Projectile
    public abstract Projectile GetProjectile();
    
    //===========================
    // Logic
    //===========================
    public override void Attack() {
        // Instantiate projectile
        GetProjectile().Shoot();
    }
}
