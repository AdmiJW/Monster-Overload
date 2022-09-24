
using System.Collections;
using UnityEngine;

public class MaskedNinja : AbstractEnemy, IWeapon {

    private IEnumerator attackCoroutine;


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

    protected override void Start() {
        base.Start();
        movement.Enabled = false;
    }
    

    //=================================
    // Event Handlers
    //=================================
    protected override void OnHurt() {
        EnemyAudioManager.instance.humanHit.Play();
    }

    protected override void OnDeath() {
        base.OnDeath();
        StopCoroutine(attackCoroutine);
        EnemyAudioManager.instance.humanDeath.Play();
        animator.SetTrigger("Die");
    }

    protected override void handleFacingDirectionChange(FaceDirection faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.unitVector.x);
        animator.SetFloat("Vertical", faceDirection.unitVector.y);
    }

    protected override void handleMovementStateChange(MovementState movementState) {
        animator.SetBool("IsWalking", movementState == MovementState.MOVING);
    }

    protected override void OnPlayerEnterRange() {
        movement.Enabled = true;
        attackCoroutine = AttackCoroutine();
        StartCoroutine(attackCoroutine);
    }

    protected override void OnPlayerExitRange() {
        movement.Enabled = false;
        movement.movementState = MovementState.IDLE;
        StopCoroutine(attackCoroutine);
    }


    //=============================
    //  Attack coroutine
    //=============================

    // For boomerang weapon, attempt to fire weapon every [cooldown] seconds, only if boomerang had been returned.
    IEnumerator AttackCoroutine() {
        float cooldown = GetWeaponData().attackCooldown + 0.1f;

        while (true) {
            yield return new WaitForSeconds( cooldown );
            OnAttackStart();
        }
    }
}
