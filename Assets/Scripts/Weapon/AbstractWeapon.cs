using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class AbstractWeapon : MonoBehaviour, IWeapon {

    [Header("General Weapon Settings")]
    public float attackCooldown = 0.5f;

    protected GameObject player;
    protected Animator animator;
    protected IDamage damageStrategy;
    protected ContactFilter2D ENEMY_CONTACT_FILTER;

    protected bool isInCooldown = false;


    public void Initialize(GameObject player, ContactFilter2D enemyContactFilter) {
        this.player = player;
        animator = player.GetComponent<Animator>();
        this.ENEMY_CONTACT_FILTER = enemyContactFilter;
    }


    protected IEnumerator Cooldown() {
        isInCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isInCooldown = false;
    }


    public abstract void TriggerAttack();
    public abstract void DealDamage();
    public abstract void PlayAttackAnimation();
}
