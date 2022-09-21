using UnityEngine;


public class EnemyRangedWeapon : AbstractRangedWeapon {

    [Header("References")]
    public Animator animator;


    //===========================
    //  Logic
    //===========================
    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }


    public override Projectile GetProjectile() {
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        Projectile p = projectile.GetComponent<Projectile>();
        p.IncludeTarget( GameManager.instance.PLAYER_LAYER_MASK );
        p.IncludeSelfDestroyTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( ItemAudioManager.instance.arrowImpact );      // TODO: Impact sound varies
        p.OrientProjectile( Util.GetAngle(direction) );
        p.SetDamageStrategy( damage );

        return p;
    }
}
