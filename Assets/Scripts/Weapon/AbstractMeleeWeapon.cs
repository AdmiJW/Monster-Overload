using System;
using UnityEngine;


// Things to implement in your concrete melee:
//      > Animation + Sound (as animation event?)
public abstract class AbstractMeleeWeapon : AbstractWeapon<MeleeWeaponData> {

    protected Collider2D hitbox;
    protected Collider2D[] enemyHits;
    protected AbstractMovement entityMovement;

    protected IDamage damageStrategy;


    // Events
    public event Action<GameObject> onHitboxEnter;


    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();
        hitbox = GetComponent<Collider2D>();
    }


    protected virtual void OnEnable() {
        entityMovement.faceDirection.onDirectionChange += UpdateDirection;
        UpdateDirection(entityMovement.faceDirection);
    }


    protected virtual void Start() {
        damageStrategy = new PhysicalDamage(transform, weaponData.attackDamage, weaponData.attackKnockback);
        enemyHits = new Collider2D[ weaponData.maxEnemiesToAttack ];
    }


    protected override void OnDisable() {
        base.OnDisable();
        entityMovement.faceDirection.onDirectionChange -= UpdateDirection;
    }


    //===========================
    //  Handlers
    //===========================
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        onHitboxEnter?.Invoke(other.gameObject);
    }

    

    //===========================
    // Logic
    //===========================
    public override void Attack() {
        for (int i = 0; i < enemyHits.Length; i++) enemyHits[i] = null;
        hitbox.OverlapCollider( targetContactFilter, enemyHits);

        foreach (Collider2D enemyHit in enemyHits) {
            if (enemyHit == null) continue;
            damageStrategy.DealDamage(enemyHit.gameObject);
        }
    }


    public void UpdateDirection(FaceDirection direction) {
        transform.rotation = Quaternion.Euler(0, 0, direction.GetAngle() );
    }
}
