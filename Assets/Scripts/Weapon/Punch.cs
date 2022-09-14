using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : AbstractMeleeWeapon {

    public override WeaponData GetWeaponData() {
        return new MeleeWeaponData {
            name = WeaponType.PUNCH,
            attackCooldown = attackCooldown,
            attackDamage = attackDamage,
            attackKnockback = attackKnockback,
            maxEnemiesToAttack = maxEnemiesToAttack
        };
    }


    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Punch");
    }



    public void LoadFromWeaponData(MeleeWeaponData data) {
        attackCooldown = data.attackCooldown;
        attackDamage = data.attackDamage;
        attackKnockback = data.attackKnockback;
        maxEnemiesToAttack = data.maxEnemiesToAttack;
    }
    
}

