
public class Sword : AbstractMeleeWeapon {

    public override WeaponData GetWeaponData() {
        return new MeleeWeaponData {
            name = WeaponType.SWORD,
            attackCooldown = attackCooldown,
            attackDamage = attackDamage,
            attackKnockback = attackKnockback,
            maxEnemiesToAttack = maxEnemiesToAttack
        };
    }


    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Sword");
    }



    public void LoadFromWeaponData(MeleeWeaponData data) {
        attackCooldown = data.attackCooldown;
        attackDamage = data.attackDamage;
        attackKnockback = data.attackKnockback;
        maxEnemiesToAttack = data.maxEnemiesToAttack;
    }
    
}

