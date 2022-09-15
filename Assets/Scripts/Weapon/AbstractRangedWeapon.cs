using UnityEngine;


public abstract class AbstractRangedWeapon : AbstractWeapon<RangedWeaponData> {

    [Header("References")]
    public GameObject projectilePrefab;


    // Method to obtain a working projectile, must contain script Projectile
    public abstract GameObject GetProjectile();
    //===========================
    // Logic
    //===========================
    public override void Attack() {
        // Instantiate projectile
        GameObject projectile = GetProjectile();
        projectile.GetComponent<Projectile>().Shoot();
    }
}
