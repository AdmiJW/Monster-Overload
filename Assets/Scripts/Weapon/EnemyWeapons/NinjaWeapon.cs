using UnityEngine;



public class NinjaWeapon : EnemyRangedWeapon {

    public override AudioSource GetProjectileImpactSound() {
        return ItemAudioManager.instance.sharpItemHit;
    }

    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }


    public override void PlayAttackSound() {
        ItemAudioManager.instance.throwItem.Play();
    }
}
