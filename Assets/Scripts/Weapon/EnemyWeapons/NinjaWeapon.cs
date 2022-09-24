using UnityEngine;
using NaughtyAttributes;


public class NinjaWeapon : AbstractRangedWeapon<RangedWeaponData> {
    
    [BoxGroup("References")]
    public Animator animator;


    public override Projectile<RangedWeaponData> GetProjectile() {
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        StraightProjectile p = projectile.GetComponent<StraightProjectile>();
        p.IncludeTarget( GameManager.instance.PLAYER_LAYER_MASK );
        p.IncludeCollideTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( ItemAudioManager.instance.sharpItemHit );
        p.OrientProjectile( Util.GetAngle(direction) );
        p.SetDamageStrategy( damage );

        return p;
    }


    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }


    public override void PlayAttackSound() {
        ItemAudioManager.instance.throwItem.Play();
    }
}
