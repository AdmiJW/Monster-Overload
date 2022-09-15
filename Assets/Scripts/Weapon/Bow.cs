

public class Bow : AbstractRangedWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Bow");
    }
}
