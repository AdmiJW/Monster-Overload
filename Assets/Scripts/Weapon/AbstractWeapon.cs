using System;
using System.Collections;
using UnityEngine;



public abstract class AbstractWeapon : MonoBehaviour {

    [Header("General Weapon Settings")]
    public float attackCooldown = 0.5f;

    protected GameObject player;
    protected Animator playerAnimator;
    protected ContactFilter2D ENEMY_CONTACT_FILTER;

    protected bool isInCooldown = false;


    protected virtual void Awake() {
        player = PlayerManager.instance.player;
        playerAnimator = player.GetComponent<Animator>();
        ENEMY_CONTACT_FILTER = GameManager.instance.ENEMY_CONTACT_FILTER;
    }


    protected IEnumerator Cooldown() {
        isInCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isInCooldown = false;
    }


    public abstract void TriggerAttack();
    public abstract void DealDamage();
    public abstract void PlayAttackAnimation();
    public abstract WeaponData GetWeaponData();
}



[System.Serializable]
public abstract class WeaponData {
    public WeaponType name;
    public float attackCooldown;
}