using UnityEngine;
using NaughtyAttributes;

public class Lizard : AbstractEnemy {

    [BoxGroup("Enemy Settings")]
    public float idleMinDuration, idleMaxDuration;
    [BoxGroup("Enemy Settings")]
    public float moveMinDuration, moveMaxDuration;


    //=============================
    // Lifecycle
    //=============================
    protected override void Awake() {
        base.Awake();

        health = new BarHealth(maxHealth, healthDisplayGroup, healthVisibleOnStart);
        knockback = new AddForceKnockback(rb, knockbackResistance);
        contactDamageStrategy = new PhysicalDamage(transform, contactDamage, contactKnockback);
        invulnerability = new NullInvulnerable();        
        
        movement = new RandomIntervalMovement(
            gameObject, 
            moveSpeed, 
            idleMinDuration, 
            idleMaxDuration, 
            moveMinDuration, 
            moveMaxDuration
        );
    }


    // Since facingHandler is only registered in OnEnable, we have to update the first direction ourselves
    protected override void Start() {
        base.Start();
        handleFacingDirectionChange(movement.faceDirection);
    }


    //=================================
    // Event Handlers
    //=================================
    protected void OnCollisionEnter2D(Collision2D collision) {
        movement.OnCollisionEnter2D(collision);
    }


    protected override void OnHurt() {
        EnemyAudioManager.instance.lizard.Play();
    }

    protected override void OnDeath() {
        base.OnDeath();
        EnemyAudioManager.instance.lizard.Play();
        animator.SetTrigger("Death");
    }


    protected override void handleMovementStateChange(MovementState movementState) {
        if (movementState == MovementState.IDLE) animator.SetTrigger("Idle");
        else animator.SetTrigger("Moving");
    }

    protected override void handleFacingDirectionChange(FaceDirection faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.unitVector.x);
        animator.SetFloat("Vertical", faceDirection.unitVector.y);
    }
}
