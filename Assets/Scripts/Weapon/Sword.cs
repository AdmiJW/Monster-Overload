
public class Sword : AbstractMeleeWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Sword");
    }
}

