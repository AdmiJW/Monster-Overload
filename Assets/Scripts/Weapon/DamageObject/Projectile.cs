using System;
using System.Collections;
using UnityEngine;


// Initialization order is important:
//      1. Target layer & self destroy layer
//      2. Weapon data
//      3. Damage strategy
//      4. Orientation, Impact sound
// Then call shoot() to fire the projectile with rigidbody
public class Projectile : DamageObject {

    protected RangedWeaponData data;
    protected AudioSource impactSound;

    protected ParticleSystem particle;
    protected SpriteRenderer spriteRenderer;
    protected LayerMask destroyLayerMask;
    


    //===========================
    // Lifecycle
    //===========================
    protected virtual void Awake() {
        particle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    //===================================
    //  Public Projectile Initializers
    //===================================
    public virtual void SetWeaponData(RangedWeaponData data) {
        this.data = data;
        UpdatePiercingProperty();
    }

    public virtual void SetImpactSound(AudioSource sound) {
        impactSound = sound;
    }

    public virtual void OrientProjectile(FaceDirection direction) {
        transform.rotation = Quaternion.Euler(0, 0, direction.GetAngle() );
    }

    public virtual Projectile Shoot() {
        if (damage == null) throw new Exception("Projectile damage strategy not set");

        GetComponent<Rigidbody2D>().velocity = transform.right * data.projectileSpeed;
        StartCoroutine( Lifetime() );
        return this;
    }


    //========================
    //  Logic
    //========================
    protected override void DestroyHandler(int layer) {
        if ( (selfDestroyLayerMask & (1 << layer) ) == 0 ) return;
        impactSound.Play();
        spriteRenderer.enabled = false;
        particle?.Play();
        Destroy(this);
    }


    // If a projectile is not piercing, then contact with enemies will result in projectile's destroy
    void UpdatePiercingProperty() {
        if (data.isPiercing) ExcludeSelfDestroyTarget(targetLayerMask);
        else IncludeSelfDestroyTarget(targetLayerMask);
    }


    

    //===========================
    //  Coroutines
    //===========================
    IEnumerator Lifetime() {
        if (data.projectileLifetime == 0) yield break;
        yield return new WaitForSeconds(data.projectileLifetime);
        Destroy(gameObject);
    }
}
