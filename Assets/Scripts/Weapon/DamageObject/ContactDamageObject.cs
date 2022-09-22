
using UnityEngine;


// A gameobject that has a collider, and directly deals damage to the target on contact.
// Great for objects like traps
public class ContactDamageObject : DamageObject {
    
    //===========================
    // Event Listeners
    //===========================
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        DamageHandler(collision.gameObject);
        DestroyHandler(collision.gameObject.layer);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        DamageHandler(collider.gameObject);
        DestroyHandler(collider.gameObject.layer);
    }


    //===========================
    //  Handler
    //===========================
    protected virtual void DamageHandler(GameObject obj) {
        if ( (targetLayerMask & (1 << obj.layer) ) == 0 ) return;
        damage.DealDamage(obj);
    }


    protected virtual void DestroyHandler(int layer) {
        if ( (selfDestroyLayerMask & (1 << layer) ) == 0 ) return;
        Destroy(gameObject);
    }
}
