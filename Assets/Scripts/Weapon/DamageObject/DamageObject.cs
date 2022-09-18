using UnityEngine;


// Attach to a gameobject with a collider/trigger, and given a layermask, will deal damage to the
// collided object.
// Use in weapon projectiles like weapon, traps, spells, etc.
//
// Also handles self destruct on collision with colliders in the destroyLayerMask
public class DamageObject : MonoBehaviour {
    
    protected IDamage damage;
    protected LayerMask targetLayerMask = 0;
    protected LayerMask selfDestroyLayerMask = 0;
    
    //===========================
    // Public 
    //===========================
    public void IncludeTarget(LayerMask layerMask) {
        targetLayerMask |= layerMask;
    }

    public void ExcludeTarget(LayerMask layerMask) {
        targetLayerMask &= ~layerMask;
    }

    public void IncludeSelfDestroyTarget(LayerMask layerMask) {
        selfDestroyLayerMask |= layerMask;
    }

    public void ExcludeSelfDestroyTarget(LayerMask layerMask) {
        selfDestroyLayerMask &= ~layerMask;
    }

    public void SetDamageStrategy(IDamage damage) {
        this.damage = damage;
    }



    //===========================
    // Event Listener
    //===========================
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        DamageHandler(collision.gameObject.layer, collision.gameObject);
        DestroyHandler(collision.gameObject.layer);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        DamageHandler(collider.gameObject.layer, collider.gameObject);
        DestroyHandler(collider.gameObject.layer);
    }


    //===========================
    //  Handler
    //===========================
    protected virtual void DamageHandler(int layer, GameObject obj) {
        if ( (targetLayerMask & (1 << layer) ) == 0 ) return;
        damage.DealDamage(obj);
    }


    protected virtual void DestroyHandler(int layer) {
        if ( (selfDestroyLayerMask & (1 << layer) ) == 0 ) return;
        Destroy(gameObject);
    }
}
