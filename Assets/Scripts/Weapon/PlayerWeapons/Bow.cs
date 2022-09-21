using UnityEngine;


public class Bow : AbstractRangedWeapon {

    private Animator animator;
    private FaceDirection playerFaceDirection;


    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();

        GameObject player = PlayerManager.instance.player;
        animator = player.GetComponent<Animator>();
        playerFaceDirection = player.GetComponent<PlayerMovementScript>().MovementStrategy.faceDirection;
    }


    //===========================
    //  Logic
    //===========================
    public override void PlayAttackAnimation() {
        animator.SetTrigger("Bow");
    }


    public override Projectile GetProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        Projectile p = projectile.GetComponent<Projectile>();
        p.IncludeTarget( GameManager.instance.ENEMY_LAYER_MASK );
        p.IncludeSelfDestroyTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( ItemAudioManager.instance.arrowImpact );
        p.OrientProjectile( playerFaceDirection.GetAngle() );
        p.SetDamageStrategy( damage );

        return p;
    }
}
