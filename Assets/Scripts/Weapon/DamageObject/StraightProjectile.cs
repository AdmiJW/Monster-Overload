

// Projectile that simply travels in a straight line, like an arrow
public class StraightProjectile : Projectile<RangedWeaponData> {
    
    public override Projectile<RangedWeaponData> Shoot() {
        StartCoroutine( Lifetime() );

        rb.velocity = transform.right * data.projectileSpeed;
        travelSound?.Play();
        return this;
    }


    protected override void CollideHandler(int layer) {
        if ( (collideLayerMask & (1 << layer) ) == 0 ) return;
        
        if (animator != null) animator.enabled = false;
        impactSound?.Play();
        travelSound?.Stop();
        spriteRenderer.enabled = false;
        particle?.Play();
        Destroy(this);
    }
}
