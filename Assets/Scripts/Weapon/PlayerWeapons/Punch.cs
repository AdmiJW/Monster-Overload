using UnityEngine;


public class Punch : AbstractMeleeWeapon {

    private Animator animator;

    protected override void Awake() {
        base.Awake();

        GameObject player = PlayerManager.instance.player;
        animator = player.GetComponent<Animator>();
        entityMovement = player.GetComponent<PlayerMovementScript>().MovementStrategy;
    }


    public override void PlayAttackAnimation() {
        animator.SetTrigger("Punch");
    }
}

