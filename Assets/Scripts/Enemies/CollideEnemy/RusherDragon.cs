using UnityEngine;
using NaughtyAttributes;


public class RusherDragon : AbstractEnemy {

    [BoxGroup("Enemy Settings")]
    public float idleDuration, warningDuration, dashDuration;


    //=============================
    // Lifecycle
    //=============================
    protected override void Awake() {
        base.Awake();

        health = new BarHealth(maxHealth, healthDisplayGroup, healthVisibleOnStart);
        knockback = new AddForceKnockback(rb, knockbackResistance);
        contactDamageStrategy = new PhysicalDamage(transform, contactDamage, contactKnockback);
        invulnerability = new NullInvulnerable();

        movement = new DashTargetMovement(
            gameObject, 
            PlayerManager.instance.player, 
            moveSpeed, 
            idleDuration, 
            warningDuration, 
            dashDuration
        );
    }


    // Since facingHandler is only registered in OnEnable, we have to update the first direction ourselves
    protected override void Start() {
        base.Start();
        handleFacingDirectionChange(movement.faceDirection);
        handleMovementStateChange(movement.movementState);
    }


    //=================================
    // Event Handlers
    //=================================
    protected override void handleMovementStateChange(MovementState movementState) {
        if (movementState == MovementState.IDLE) {
            animator.SetTrigger("Idle");
        }
        else if (movementState == MovementState.WARNING) {
            ItemAudioManager.instance.spellCharging.Play();
            animator.SetTrigger("Warning");
        } 
        else {
            ItemAudioManager.instance.dash.Play();
            animator.SetTrigger("Dashing");
        }
    }


    protected void OnCollisionEnter2D(Collision2D collision) {
        if ( movement.movementState == MovementState.MOVING ) {
            ItemAudioManager.instance.heavyImpact.Play();
            CameraManager.instance.ShakeCamera(0.3f, 1f, 150);
        }

        movement.OnCollisionEnter2D(collision);
    }


    protected override void OnHurt() {
        EnemyAudioManager.instance.humanHit.Play();
    }

    protected override void OnDeath() {
        base.OnDeath();
        EnemyAudioManager.instance.dragonDeath.Play();
        animator.SetTrigger("Death");
    }

    protected override void handleFacingDirectionChange(FaceDirection faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.unitVector.x);
        animator.SetFloat("Vertical", faceDirection.unitVector.y);
    }
}
