using System.Collections;
using UnityEngine;
using NaughtyAttributes;



// * Base class that contains weapon data and target layer masks.
// *
// * This abstract class implements weapon cooldown behavior to prevent rapid firing, and also
// * prevents weapon from firing twice due to animation blend tree running 2+ animation clips

public abstract class AbstractWeapon<T> : MonoBehaviour, IWeapon where T: WeaponData {

    [BoxGroup("References")]
    public T weaponData;

    [Space(20)]
    [SerializeField]
    private LayerMask[] targetLayerMasks;

    protected LayerMask compositeTargetLayerMask = 0;
    protected ContactFilter2D targetContactFilter; 
    
    private bool attackLock = false;
    private IEnumerator cooldownCoroutine = null;


    //=========================================================================
    // Abstract method - Every subclass must implement the weapon behaviour
    //=========================================================================
    public abstract void Attack();               
    public abstract void PlayAttackAnimation();  
    public abstract void PlayAttackSound();



    //===========================
    //  Lifecycle
    //===========================
    protected virtual void Awake() {
        foreach (LayerMask layerMask in targetLayerMasks) 
            compositeTargetLayerMask |= layerMask;

        targetContactFilter = new ContactFilter2D();
        targetContactFilter.SetLayerMask(compositeTargetLayerMask);
    }

    // When weapon is disabled (player switching weapon), disable weapon cooldown immediately
    protected virtual void OnDisable() {
        if (cooldownCoroutine != null) StopCoroutine(cooldownCoroutine);
        cooldownCoroutine = null;
    }


    //===========================
    //  Logic
    //===========================
    protected IEnumerator Cooldown() {
        yield return new WaitForSeconds(weaponData.attackCooldown);
        cooldownCoroutine = null;
    }

    public WeaponData GetWeaponData() {
        return weaponData;
    }


    public virtual void OnAttackStart() {
        if (cooldownCoroutine != null) return;

        PlayAttackAnimation();
        cooldownCoroutine = Cooldown();
        StartCoroutine(Cooldown());
    }

    public virtual void OnAttackPerform() {
        if (attackLock) return;
        attackLock = true;

        Attack();
        PlayAttackSound();
    }

    public virtual void OnAttackEnd() {
        attackLock = false;
    }
}