

public class Slime2 : AbstractEnemy {

    //=============================
    // Lifecycle
    //=============================
    protected override void Awake() {
        base.Awake();

        health = new BarHealth(maxHealth, healthDisplayGroup, healthVisibleOnStart);
        knockback = new AddForceKnockback(rb, knockbackResistance);
        contactDamageStrategy = new PhysicalDamage(transform, contactDamage, contactKnockback);
        invulnerability = new NullInvulnerable();        
        movement = new ChaseObjectMovement( PlayerManager.instance.player, gameObject, moveSpeed );
    }


    //=================================
    // Event Handlers
    //=================================
    protected override void OnHurt() {
        EnemyAudioManager.instance.slimeImpact.Play();
    }

    protected override void OnDeath() {
        base.OnDeath();
        EnemyAudioManager.instance.slimeImpact.Play();
        animator.SetTrigger("Death");
    }

    protected override void handleFacingDirectionChange(FaceDirection faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.unitVector.x);
        animator.SetFloat("Vertical", faceDirection.unitVector.y);
    }

    protected override void OnPlayerEnterRange() {
        movement.Enabled = true;
    }

    protected override void OnPlayerExitRange() {
        movement.Enabled = false;
    }
}
