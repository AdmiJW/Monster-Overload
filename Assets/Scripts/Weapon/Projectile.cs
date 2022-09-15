using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Needs to be initialized from builder functions below
    protected RangedWeaponData data;
    protected LayerMask targetLayerMask = 0;
    protected IDamage damage;
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
    public virtual Projectile SetWeaponData(RangedWeaponData data) {
        this.data = data;
        return this;
    }

    public virtual Projectile SetImpactSound(AudioSource sound) {
        impactSound = sound;
        return this;
    }

    public virtual Projectile SetTargetLayerMask(LayerMask targetLayerMask) {
        this.targetLayerMask = targetLayerMask;
        UpdateDestroyLayerMask();
        return this;
    }

    public virtual Projectile UsePhysicalDamageStrategy() {
        if (data == null) throw new Exception("Projectile data not set");
        damage = new PhysicalDamage(transform, data.projectileAttack, data.projectileKnockback);
        return this;
    }

    public virtual Projectile OrientProjectile(FaceDirection direction) {
        transform.rotation = Quaternion.Euler(0, 0, direction.GetAngle() );
        return this;
    }

    public virtual Projectile Shoot() {
        GetComponent<Rigidbody2D>().velocity = transform.right * data.projectileSpeed;
        StartCoroutine( Lifetime() );
        return this;
    }


    //===========================
    //  Handler
    //===========================
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        LayerMask collisionLayerMask = 1 << collision.gameObject.layer;

        HandleProjectileDamage(collisionLayerMask, collision.gameObject);
        HandleProjectileDestroy(collisionLayerMask);
    }


    //========================
    //  Logic
    //========================
    protected virtual void HandleProjectileDamage(LayerMask collided, GameObject collidedObject) {
        if ( (collided & targetLayerMask) == 0) return;
        damage.DealDamage(collidedObject);
    }

    protected virtual void HandleProjectileDestroy(LayerMask collided) {
        if ( (collided & destroyLayerMask) == 0 ) return;

        impactSound.Play();
        spriteRenderer.enabled = false;
        particle?.Play();
        Destroy(this);
    }


    // Update destroy layer mask to include the target layer mask or not via isPiercing
    protected virtual void UpdateDestroyLayerMask() {
        destroyLayerMask = data.isPiercing?
            GameManager.instance.MAP_LAYER_MASK:
            (LayerMask)(GameManager.instance.MAP_LAYER_MASK | targetLayerMask);
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
