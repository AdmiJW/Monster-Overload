using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;


// Projectile that goes back and fourth.
// The rigidbody shall be set to type kinematic.
public class BoomerangProjectile : Projectile<BoomerangWeaponData> {

    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public Transform origin;

    private bool isReturning = false;
    private float velocity;
    private float timer;
    private float force;

    public event Action onBoomerangReturn;
    

    //===================================
    //  Public Projectile Initializers
    //===================================
    public virtual void SetOrigin(Transform origin) {
        this.origin = origin;
    }



    //===========================
    // Lifecycle
    //===========================
    protected virtual void FixedUpdate() {
        if (!isReturning) AwayStateUpdate();
        else ReturnStateUpdate();
    }
    

    protected virtual void OnDisable() {
        travelSound?.Stop();
    }



    //===========================
    //  Logic
    //===========================
    public override Projectile<BoomerangWeaponData> Shoot() {
        StartCoroutine( Lifetime() );
        
        timer = data.travelTime;
        force = data.projectileSpeed / data.travelTime;
        velocity = data.projectileSpeed;
        travelSound?.Play();
        return this;
    }



    // On collide with destroy layer, don't destroy the boomerang, but instead transition to return state
    protected override void CollideHandler(int layer) {
        if ( (collideLayerMask & (1 << layer) ) == 0 ) return;
        
        impactSound?.Play();
        particle?.Play();
        isReturning = true;
    }


    protected override IEnumerator Lifetime() {
        if (data.projectileLifetime == 0) yield break;
        yield return new WaitForSeconds(data.projectileLifetime);
        onBoomerangReturn?.Invoke();
        Destroy(gameObject);
    }



    //===========================
    //  State Handler
    //===========================
    void AwayStateUpdate() {
        timer -= Time.fixedDeltaTime;
        isReturning = timer <= 0;

        velocity = Mathf.Max(0, velocity - force * Time.fixedDeltaTime);
        rb.velocity = velocity * transform.right;
    }
    

    void ReturnStateUpdate() {
        Vector2 towardsOrigin = ( (Vector2)origin.position - rb.position ).normalized;
        velocity += force * Time.fixedDeltaTime;
        rb.velocity = velocity * towardsOrigin;

        if (Vector2.Distance(rb.position, origin.position) < 1f) {
            travelSound?.Stop();
            onBoomerangReturn?.Invoke();
            Destroy(gameObject);
        }
    }
}
