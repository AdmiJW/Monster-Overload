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
    
    protected Health health;
    protected IKnockback knockback;
    protected IDamage contactDamageStrategy;
    protected IInvulnerable invulnerability;
    protected AbstractMovement movement;
    protected DropEmitter dropEmitter;


    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rangeTriggerScript = GetComponentInChildren<RangeTriggerScript>();
        dropEmitter = GetComponentInChildren<DropEmitter>();
    }


    protected virtual void FixedUpdate() {
        movement.Move();
    }

    

    protected void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) 
            contactDamageStrategy.DealDamage(collision.gameObject);
    }



    // If an enemy is not physical, it cannot be collided and does not move.
    // Primarily use is when an enemy is dead, but needs some death animation
    protected void SetPhysical(bool isPhysical) {
        movement.Enabled = isPhysical;
        contactDamageStrategy.SetActive(isPhysical);

        rb.isKinematic = !isPhysical;
        if (!isPhysical) rb.velocity = Vector2.zero;
        enemyCollider.enabled = isPhysical;
    }


    //================================
    // Interface methods
    //================================
    public void TakeDamage(float damage) { 
        health.TakeDamage(damage); 
    }

    public void Heal(float amount) { 
        health.Heal(amount); 
    }

    public void SetMaxHealth(float maxHealth, bool healToFull = false) { 
        health.SetMaxHealth(maxHealth, healToFull);
    }

    public void SetHealth(float health) { 
        this.health.SetHealth(health); 
    }

    public float GetHealth() {
        return health.GetHealth(); 
    }

    public void Knockback(Vector2? origin = null, float knockback = 0) { 
        this.knockback.Knockback(origin, knockback); 
    }

    public bool IsInvulnerable() {
        return invulnerability.IsInvulnerable(); 
    }

    public void ActivateVulnerable() {
        invulnerability.ActivateVulnerable(); 
    }
}
