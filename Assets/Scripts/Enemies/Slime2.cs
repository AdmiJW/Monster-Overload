
using UnityEngine;


public class Slime2 : AbstractEnemy {

    [Header("Enemy Drops")]
    public int minCoinDrop = 1;
    public int maxCoinDrop = 3;



    protected override void Awake() {
        base.Awake();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        healthStrategy = new BarHealth(maxHealth, healthDisplayGroup, healthVisibleOnStart);
        knockbackStrategy = new AddForceKnockback(rb, knockbackResistance);
        contactDamageStrategy = new PhysicalDamage(transform, contactDamage, contactKnockback);
        invulnerableStrategy = new NullInvulnerable();
        monsterDropEmitter = new MonsterDropEmitter(transform, minCoinDrop, maxCoinDrop);
        movementStrategy = new ChaseObjectMovement(player, gameObject, moveSpeed);
    }

    void Start() {
        healthStrategy.OnHurt += ()=> EnemyAudioManager.instance.slimeImpact.Play();
        healthStrategy.OnDeath += OnDeath;

        // Only chase player when in range
        if (rangeTriggerScript == null) return;
        movementStrategy.Enabled = false;
        rangeTriggerScript.onPlayerEnter += () => movementStrategy.Enabled = true;
        rangeTriggerScript.onPlayerExit += () => movementStrategy.Enabled = false;
    }

    void OnEnable() {
        movementStrategy.faceDirection.onDirectionChange += handleFacingDirectionChange;
    }

    void OnDisable() {
        movementStrategy.faceDirection.onDirectionChange -= handleFacingDirectionChange;
    }


    


    // When the smoke effect ends
    void OnSmokeEffectEnd() {
        monsterDropEmitter.Activate();
        // Delayed destroy to wait for healthbar tween to complete
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }


    void OnDeath() {
        SetPhysical(false);
        EnemyAudioManager.instance.slimeImpact.Play();
        animator.SetTrigger("Death");
    }


    protected void handleFacingDirectionChange(Vector2 faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.x);
        animator.SetFloat("Vertical", faceDirection.y);
    }   

}
