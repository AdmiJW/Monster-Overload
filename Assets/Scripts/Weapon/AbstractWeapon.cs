using System;
using System.Collections;
using UnityEngine;



public abstract class AbstractWeapon<T> : MonoBehaviour, IWeapon where T: WeaponData {

    [Header("Weapon Data")]
    public T weaponData;
    public LayerMask[] targetLayerMasks;

    protected LayerMask compositeTargetLayerMask = 0;
    protected ContactFilter2D targetContactFilter; 
    
    private IEnumerator cooldownCoroutine = null;


    //===========================
    //  Lifecycle
    //===========================
    protected virtual void Awake() {
        foreach (LayerMask layerMask in targetLayerMasks) 
            compositeTargetLayerMask |= layerMask;

        targetContactFilter = new ContactFilter2D();
        targetContactFilter.SetLayerMask(compositeTargetLayerMask);
    }

    // When weapon is switched, disable weapon cooldown immediately
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


    // Cooldown + Animation
    public virtual void OnAttackPerformed() {
        if (cooldownCoroutine != null) return;

        PlayAttackAnimation();
        cooldownCoroutine = Cooldown();
        StartCoroutine(Cooldown());
    }


    public abstract void Attack();
    public abstract void PlayAttackAnimation();
}