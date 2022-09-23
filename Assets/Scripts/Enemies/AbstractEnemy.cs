using UnityEngine;
using NaughtyAttributes;


// Abstract enemy class, all enemies should inherit from this class and have it attached on them
// What you have to do:
//      > Call base class Awake() (If you override it)
//      > Assign strategies and its extensions
//      > Register listeners to strategies and objects:
//          > onHurt, onDeath in IHealth
//          > facingDirectionChange in AbstractMovement
//          > Player enter and exit event from RangeTriggerScript.
public abstract class AbstractEnemy : MonoBehaviour, IHealth, IKnockback, IInvulnerable, IWeapon {
    [BoxGroup("Enemy Settings")]
    public float maxHealth = 100f;
    [BoxGroup("Enemy Settings")]
    public float moveSpeed = 1f;
    [BoxGroup("Enemy Settings")]
    public float contactDamage = 1f;
    [BoxGroup("Enemy Settings")]
    public float contactKnockback = 1f;
    [BoxGroup("Enemy Settings")]
    public float knockbackResistance = 0f;
    [BoxGroup("Enemy Settings")]
    public bool healthVisibleOnStart = false;


    [BoxGroup("References")]
    public GameObject healthDisplayGroup;
    [SerializeField]
    [BoxGroup("References")]
    private GameObject weaponGameObject = null;
    [BoxGroup("References")]
    public AbstractRangeTrigger rangeTrigger;
    [BoxGroup("References")]
    public DropEmitter dropEmitter;


    protected Rigidbody2D rb;
    protected Collider2D enemyCollider;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem hurtParticle;
    
    protected Health health;
    protected IKnockback knockback;
    protected IDamage contactDamageStrategy;
    protected IInvulnerable invulnerability;
    protected AbstractMovement movement;
    protected IWeapon weapon;


    //=============================
    // Lifecycle
    //=============================
    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hurtParticle = GetComponent<ParticleSystem>();

        weapon = weaponGameObject.GetComponent<IWeapon>();
    }

    protected virtual void OnEnable() {
        movement.faceDirection.onDirectionChange += handleFacingDirectionChange;
        movement.onMovementStateChange += handleMovementStateChange;

        rangeTrigger.RegisterEnterEvent(OnPlayerEnterRange);
        rangeTrigger.RegisterExitEvent(OnPlayerExitRange);
    }

    protected virtual void Start() {
        health.OnHurt += OnHurt;
        health.OnDeath += OnDeath;
    }

    protected virtual void FixedUpdate() {
        movement.Move( Time.fixedDeltaTime );
    }

    protected virtual void OnDisable() {
        movement.faceDirection.onDirectionChange -= handleFacingDirectionChange;
        movement.onMovementStateChange -= handleMovementStateChange;

        rangeTrigger.UnregisterEnterEvent(OnPlayerEnterRange);
        rangeTrigger.UnregisterExitEvent(OnPlayerExitRange);
    }


    
    //=============================
    // Event Handlers
    //=============================
    protected virtual void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) 
            OnPlayerContact(collision.gameObject);
    }

    // Override if required
    protected virtual void handleFacingDirectionChange(FaceDirection faceDirection) {}
    protected virtual void handleMovementStateChange(MovementState movementState) {}
    protected virtual void OnPlayerEnterRange() {}
    protected virtual void OnPlayerExitRange() {}
    protected virtual void OnHurt() {}

    protected virtual void OnDeath() {
        SetPhysical(false);
    }

    protected virtual void OnPlayerContact(GameObject player) {
        contactDamageStrategy.DealDamage(player);
    }

    protected virtual void OnDeathAnimationEnd() {
        dropEmitter.Activate();
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }


    //=============================
    // Logic
    //=============================
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

    // IHealth
    public void TakeDamage(float damage) { 
        health.TakeDamage(damage);
        hurtParticle?.Play();
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

    // IKnockback
    public void Knockback(Vector2? origin = null, float knockback = 0) { 
        this.knockback.Knockback(origin, knockback); 
    }

    // IInvulnerability 
    public bool IsInvulnerable() {
        return invulnerability.IsInvulnerable(); 
    }

    public void ActivateVulnerable() {
        invulnerability.ActivateVulnerable(); 
    }

    // IWeapon
    public virtual void OnAttackStart() {
        weapon?.OnAttackStart();
    }

    public virtual void OnAttackPerform() {
        weapon?.OnAttackPerform();
    }

    public virtual void OnAttackEnd() {
        weapon?.OnAttackEnd();
    }

    public WeaponData GetWeaponData() {
        return weapon?.GetWeaponData();
    }
}
