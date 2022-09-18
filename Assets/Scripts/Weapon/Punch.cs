
public class Punch : AbstractMeleeWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Punch");
    }
}

