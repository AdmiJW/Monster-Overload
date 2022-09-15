using System;
using System.Collections;
using UnityEngine;



public abstract class AbstractWeapon<T> : MonoBehaviour, IWeapon where T: WeaponData {

    [Header("Weapon Data")]
    public T weaponData;

    protected GameObject player;
    protected Animator playerAnimator;
    protected ContactFilter2D ENEMY_CONTACT_FILTER;

    protected IEnumerator cooldownCoroutine = null;


    //===========================
    //  Lifecycle
    //===========================
    protected virtual void Awake() {
        player = PlayerManager.instance.player;
        playerAnimator = player.GetComponent<Animator>();
        ENEMY_CONTACT_FILTER = GameManager.instance.ENEMY_CONTACT_FILTER;
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