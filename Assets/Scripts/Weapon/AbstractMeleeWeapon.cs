using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Things to implement in your concrete melee:
//      > Animation + Sound (as animation event?)
public abstract class AbstractMeleeWeapon : AbstractWeapon
{
    [Header("Melee Weapon Settings")]
    public float attackDamage = 1f;
    public float attackKnockback = 1f;
    public int maxEnemiesToAttack = 1;

    protected Collider2D hitbox;
    protected Collider2D[] enemyHits;


    protected void Awake() {
        hitbox = GetComponent<Collider2D>();
        damageStrategy = new PhysicalDamage(transform, attackDamage, attackKnockback);
        enemyHits = new Collider2D[maxEnemiesToAttack];
    }

    protected void Start() {
        AbstractMovement movement = player.GetComponent<PlayerMovementScript>().MovementStrategy;
        // Listen to direction change, since it is melee weapon
        movement.AddFacingDirectionListener(UpdateDirection);
        // Initialize melee hitbox with player's direction
        UpdateDirection(movement.FaceDirection.FacingDirection);
    }

    
    public override void TriggerAttack() {
        if (isInCooldown) return;
        PlayAttackAnimation();
        StartCoroutine(Cooldown());
    }


    public override void DealDamage() {
        for (int i = 0; i < enemyHits.Length; i++) enemyHits[i] = null;
        hitbox.OverlapCollider(ENEMY_CONTACT_FILTER, enemyHits);

        foreach (Collider2D enemyHit in enemyHits) {
            if (enemyHit == null) continue;
            damageStrategy.DealDamage(enemyHit.gameObject);
        }
    }


    public void UpdateDirection(Vector2 direction)
    {
        // High school math: Positive x direction = 0 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle = 360f + angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
