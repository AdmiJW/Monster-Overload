using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract enemy class, all enemies should inherit from this class and have it attached on them
// What you have to do:
//      > Call base class Awake() (If you override it)
//      > Assign strategies and its extensions
//      > Register listeners to strategies and objects:
//          > onHurt, onDeath in IHealth
//          > facingDirectionChange in AbstractMovement
//          > Player enter and exit event from RangeTriggerScript.
public abstract class AbstractEnemy : MonoBehaviour, IHealth, IKnockback, IInvulnerable {
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    public float moveSpeed = 1f;
    public float contactDamage = 1f;
    public float contactKnockback = 1f;
    public float knockbackResistance = 0f;
    
    [Header("Health")]
    public GameObject healthDisplayGroup;
    public bool healthVisibleOnStart = false;


    protected Rigidbody2D rb;
    protected Collider2D enemyCollider;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected RangeTriggerScript rangeTriggerScript;
    
    protected Health healthStrategy;
    protected IKnockback knockbackStrategy;
    protected IDamage contactDamageStrategy;
    protected IInvulnerable invulnerableStrategy;
    protected AbstractMovement movementStrategy;
    protected MonsterDropEmitter monsterDropEmitter;


    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rangeTriggerScript = transform.Find("RangeTrigger")?.GetComponent<RangeTriggerScript>();
    }


    protected virtual void FixedUpdate() {
        movementStrategy.Move();
    }

    

    protected void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) 
            contactDamageStrategy.DealDamage(collision.gameObject);
    }



    // If an enemy is not physical, it cannot be collided and does not move.
    // Primarily use is when an enemy is dead, but needs some death animation
    protected void SetPhysical(bool isPhysical) {
        movementStrategy.Enabled = isPhysical;
        contactDamageStrategy.SetActive(isPhysical);

        rb.isKinematic = !isPhysical;
        if (!isPhysical) rb.velocity = Vector2.zero;
        enemyCollider.enabled = isPhysical;
    }


    //================================
    // Interface methods
    //================================
    public void TakeDamage(float damage) { 
        healthStrategy.TakeDamage(damage); 
    }

    public void Heal(float amount) { 
        healthStrategy.Heal(amount); 
    }

    public void SetMaxHealth(float maxHealth) { 
        healthStrategy.SetMaxHealth(maxHealth); 
    }

    public float GetHealth() {
        return healthStrategy.GetHealth(); 
    }

    public void Knockback(Vector2? origin = null, float knockback = 0) { 
        knockbackStrategy.Knockback(origin, knockback); 
    }

    public bool IsInvulnerable() {
        return invulnerableStrategy.IsInvulnerable(); 
    }

    public void ActivateVulnerable() {
        invulnerableStrategy.ActivateVulnerable(); 
    }
}
