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
        int hits = hitbox.OverlapCollider( targetContactFilter, enemyHits);

        for ( int i = 0; i < hits; ++i ) 
            damageStrategy.DealDamage( enemyHits[i].gameObject);
    }


    public void UpdateDirection(FaceDirection direction) {
        transform.rotation = Quaternion.Euler(0, 0, direction.GetAngle() );
    }
}
