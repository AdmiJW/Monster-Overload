using System.Collections;
using UnityEngine;



// A projectile is a contact damage object that travels in a certain path. Eg: arrow, boomerang...
// The projectile must process:
//      - Rigidbody, dynamic or isKinematic based on concrete implementation
//      - Collider, is trigger.

// Initialization order is important:
//      1. Target layer & collide layer
//      2. Weapon data
//      3. Damage strategy
//      4. Orientation, Impact sound
// Then call shoot() to fire the projectile with rigidbody
public abstract class Projectile<T> : ContactDamageObject where T: RangedWeaponData {

    protected T data;

    protected ParticleSystem particle;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    
    protected AudioSource travelSound;
    protected AudioSource impactSound;



    //============================
    // Abstract Methods
    //============================
    public abstract Projectile<T> Shoot();

    //===========================
    // Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();
        
        particle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    //===================================
    //  Public Projectile Initializers
    //===================================
    public virtual void SetWeaponData(T data) {
        this.data = data;
        UpdatePiercingProperty();
    }

    public virtual void SetImpactSound(AudioSource sound) {
        impactSound = sound;
    }

    public virtual void SetTravelSound(AudioSource sound) {
        travelSound = sound;
    }


    // The projectile will be shoot according to transform.right. So getting the orientation is important
    public virtual void OrientProjectile(float angle) {
        transform.rotation = Quaternion.Euler(0, 0, angle );
    }


    //========================
    //  Logic
    //========================
    // If a projectile is not piercing, then contact with enemies will result in projectile's collision handler aswell
    protected void UpdatePiercingProperty() {
        if (data.isPiercing) ExcludeCollideTarget(targetLayerMask);
        else IncludeCollideTarget(targetLayerMask);
    }

    //===========================
    //  Coroutines
    //===========================
    protected virtual IEnumerator Lifetime() {
        if (data.projectileLifetime == 0) yield break;
        yield return new WaitForSeconds(data.projectileLifetime);
        Destroy(gameObject);
    }
}
