using UnityEngine;
using NaughtyAttributes;


// A script for gameobjects that will deal damage to other gameobjects. 
// Eg: Traps, spells, arrows, or even enemies
// 
// Target layer masks include the layer which the damage object will deal damage to
// Collide layer masks include the layer which the damage object will react to upon collision. Eg: self destroy, rebounce...
public abstract class DamageObject : MonoBehaviour {

    
    [BoxGroup("Initialize")]
    public bool initializeFromInspector = false;

    // Initialize from inspector
    [SerializeField]
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    private LayerMask[] targetLayerMasks, collideLayerMasks;
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public float contactDamage;
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public float contactKnockback;
    

    protected IDamage damage;
    protected LayerMask targetLayerMask = 0;
    protected LayerMask collideLayerMask = 0;
    
    //===========================
    // Public 
    //===========================
    protected virtual void Awake() {
        InitializeFromInspector();
    }

    public void IncludeTarget(LayerMask layerMask) {
        targetLayerMask |= layerMask;
    }

    public void ExcludeTarget(LayerMask layerMask) {
        targetLayerMask &= ~layerMask;
    }

    public void IncludeCollideTarget(LayerMask layerMask) {
        collideLayerMask |= layerMask;
    }

    public void ExcludeCollideTarget(LayerMask layerMask) {
        collideLayerMask &= ~layerMask;
    }

    public void SetDamageStrategy(IDamage damage) {
        this.damage = damage;
    }



    //====================================
    // Initialize from inspector
    //====================================
    void InitializeFromInspector() {
        if (!initializeFromInspector) return;

        foreach (LayerMask mask in targetLayerMasks) IncludeTarget(mask);
        foreach (LayerMask mask in collideLayerMasks) IncludeCollideTarget(mask);
        damage = new PhysicalDamage(transform, contactDamage, contactKnockback);
    }
}
