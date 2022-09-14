
using UnityEngine;


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


    void Start() {
        health.OnHurt += ()=> EnemyAudioManager.instance.slimeImpact.Play();
        health.OnDeath += OnDeath;
        movement.Enabled = false;
    }


    void OnEnable() {
        movement.faceDirection.onDirectionChange += handleFacingDirectionChange;

        if (rangeTriggerScript == null) return;
        rangeTriggerScript.onPlayerEnter += OnPlayerEnterRange;
        rangeTriggerScript.onPlayerExit += OnPlayerExitRange;
    }

    void OnDisable() {
        movement.faceDirection.onDirectionChange -= handleFacingDirectionChange;

        if (rangeTriggerScript == null) return;
        rangeTriggerScript.onPlayerEnter -= OnPlayerEnterRange;
        rangeTriggerScript.onPlayerExit -= OnPlayerExitRange;
    }
    

    //=================================
    // Event Handlers
    //=================================
    void OnDeath() {
        SetPhysical(false);
        EnemyAudioManager.instance.slimeImpact.Play();
        animator.SetTrigger("Death");
    }


    void OnSmokeEffectEnd() {
        dropEmitter.Activate();
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }


    void OnPlayerEnterRange() {
        movement.Enabled = true;
    }

    void OnPlayerExitRange() {
        movement.Enabled = false;
    }


    protected void handleFacingDirectionChange(Vector2 faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.x);
        animator.SetFloat("Vertical", faceDirection.y);
    }
}
