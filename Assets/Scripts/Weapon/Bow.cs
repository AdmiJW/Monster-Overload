using UnityEngine;


public class Bow : AbstractRangedWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Bow");
    }

    public override Projectile GetProjectile() {
        FaceDirection direction = player.GetComponent<PlayerMovementScript>().MovementStrategy.faceDirection;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        Projectile p = projectile.GetComponent<Projectile>();
        p.IncludeTarget( GameManager.instance.ENEMY_LAYER_MASK );
        p.IncludeSelfDestroyTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( ItemAudioManager.instance.arrowImpact );
        p.OrientProjectile(direction);
        p.SetDamageStrategy( damage );

        return p;
    }
}
