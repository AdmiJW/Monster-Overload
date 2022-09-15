
using UnityEngine;


// Things to implement in your concrete melee:
//      > Animation + Sound (as animation event?)
public abstract class AbstractMeleeWeapon : AbstractWeapon<MeleeWeaponData> {

    protected Collider2D hitbox;
    protected Collider2D[] enemyHits;
    protected AbstractMovement playerMovement;

    protected IDamage damageStrategy;


    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();

        hitbox = GetComponent<Collider2D>();
        playerMovement = player.GetComponent<PlayerMovementScript>().MovementStrategy;
    }


    protected virtual void OnEnable() {
        playerMovement.faceDirection.onDirectionChange += UpdateDirection;
        UpdateDirection(playerMovement.faceDirection.direction);
    }


    protected virtual void Start() {
        damageStrategy = new PhysicalDamage(transform, weaponData.attackDamage, weaponData.attackKnockback);
        enemyHits = new Collider2D[ weaponData.maxEnemiesToAttack ];
    }


    protected override void OnDisable() {
        base.OnDisable();
        playerMovement.faceDirection.onDirectionChange -= UpdateDirection;
    }

    

    //===========================
    // Logic
    //===========================
    public override void TriggerAttack() {
        if (cooldownCoroutine != null) return;
        PlayAttackAnimation();
        cooldownCoroutine = Cooldown();
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


    public void UpdateDirection(Vector2 direction) {
        // uses positive x direction = 0 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle = 360f + angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
