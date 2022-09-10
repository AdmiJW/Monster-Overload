using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime2 : AbstractEnemy {

    [Header("Enemy Settings")]
    public float movementSpeed = 20f;
    public float range = 10f;

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
        movementStrategy = new ChaseObjectMovement(player, gameObject, movementSpeed)
            .WithFaceDirection()
            .AddFacingDirectionListener(handleFacingDirectionChange);
    }


    void Start() {
        if (rangeTriggerScript == null) return;

        // Only chase player when in range
        movementStrategy.Enabled = false;
        rangeTriggerScript.onPlayerEnter += () => movementStrategy.Enabled = true;
        rangeTriggerScript.onPlayerExit += () => movementStrategy.Enabled = false;

        healthStrategy.OnHurt += ()=> AudioManager.enemyAudio.slimeImpact.Play();
        healthStrategy.OnDeath += OnDeath;
    }


    // When the smoke effect ends
    void OnSmokeEffectEnd() {
        monsterDropEmitter.Activate();
        // Delayed destroy for tween to complete
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }


    void OnDeath() {
        AudioManager.enemyAudio.slimeImpact.Play();

        movementStrategy.Enabled = false;
        contactDamageStrategy.SetActive(false);

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        animator.SetTrigger("Death");
    }


    protected void handleFacingDirectionChange(Vector2 faceDirection) {
        animator.SetFloat("Horizontal", faceDirection.x);
        animator.SetFloat("Vertical", faceDirection.y);
    }   

}
