using UnityEngine;


public class Bow : AbstractRangedWeapon<RangedWeaponData> {

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

    public override void PlayAttackSound() {
        PlayerAudioManager.instance.bow.Play();
    }


    public override Projectile<RangedWeaponData> GetProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        StraightProjectile p = projectile.GetComponent<StraightProjectile>();
        p.IncludeTarget( GameManager.instance.ENEMY_LAYER_MASK );
        p.IncludeCollideTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( ItemAudioManager.instance.arrowImpact );
        p.OrientProjectile( playerFaceDirection.GetAngle() );
        p.SetDamageStrategy( damage );

        return p;
    }
}
