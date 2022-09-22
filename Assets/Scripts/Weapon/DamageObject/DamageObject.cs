using UnityEngine;
using NaughtyAttributes;

// A script for gameobjects that will deal damage to other gameobjects. 
// Eg: Traps, spells, or even enemies.
public abstract class DamageObject : MonoBehaviour {

    
    [BoxGroup("Initialize")]
    public bool initializeFromInspector = false;

    // Initialize from inspector
    [SerializeField]
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    private LayerMask[] targetLayerMasks, selfDestroyLayerMasks;
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public float contactDamage;
    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public float contactKnockback;
    

    protected IDamage damage;
    protected LayerMask targetLayerMask = 0;
    protected LayerMask selfDestroyLayerMask = 0;
    
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

    public void IncludeSelfDestroyTarget(LayerMask layerMask) {
        selfDestroyLayerMask |= layerMask;
    }

    public void ExcludeSelfDestroyTarget(LayerMask layerMask) {
        selfDestroyLayerMask &= ~layerMask;
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
        foreach (LayerMask mask in selfDestroyLayerMasks) IncludeSelfDestroyTarget(mask);
        damage = new PhysicalDamage(transform, contactDamage, contactKnockback);
    }
}
