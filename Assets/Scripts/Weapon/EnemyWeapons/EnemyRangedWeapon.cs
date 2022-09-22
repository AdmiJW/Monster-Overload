using UnityEngine;


public abstract class EnemyRangedWeapon : AbstractRangedWeapon {

    [Header("References")]
    public Animator animator;
    
    private AudioSource projectileImpactSound;


    //========================
    // Abstract methods
    //========================
    public abstract AudioSource GetProjectileImpactSound();


    //========================
    // Lifecycle
    //========================
    protected override void Awake() {
        base.Awake();
        projectileImpactSound = GetProjectileImpactSound();
    }


    //========================
    // Logic
    //========================
    public override Projectile GetProjectile() {
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(projectile.transform, weaponData.projectileAttack, weaponData.projectileKnockback);

        Projectile p = projectile.GetComponent<Projectile>();
        p.IncludeTarget( GameManager.instance.PLAYER_LAYER_MASK );
        p.IncludeSelfDestroyTarget( GameManager.instance.MAP_LAYER_MASK );
        p.SetWeaponData(weaponData);
        p.SetImpactSound( projectileImpactSound );
        p.OrientProjectile( Util.GetAngle(direction) );
        p.SetDamageStrategy( damage );

        return p;
    }
}
