using System.Collections;
using UnityEngine;
using NaughtyAttributes;


public class MaskedNinjaWeapon : AbstractRangedWeapon<BoomerangWeaponData> {
    
    [BoxGroup("References")]
    public Animator animator;


    public override Projectile<BoomerangWeaponData> GetProjectile() {
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        BoomerangProjectile p = projectile.GetComponent< BoomerangProjectile >();
        p.IncludeTarget( GameManager.instance.PLAYER_LAYER_MASK );
        p.IncludeCollideTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetOrigin( transform );
        p.SetImpactSound( ItemAudioManager.instance.sharpItemHit );
        p.SetTravelSound( ItemAudioManager.instance.airLoopFast );
        p.OrientProjectile( Util.GetAngle(direction) );
        p.SetDamageStrategy( damage );

        // When boomerang return, only remove the cooldown
        p.onBoomerangReturn += ()=> cooldownCoroutine = null;

        return p;
    }


    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }


    public override void PlayAttackSound() {
        ItemAudioManager.instance.throwItem.Play();
    }


    // Weapon cooldown doesn't apply here. The weapon need to return to the enemy before it can be used again.
    protected override IEnumerator Cooldown() { yield break; }
}
