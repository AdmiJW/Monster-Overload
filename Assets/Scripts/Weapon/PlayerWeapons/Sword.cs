using UnityEngine;


public class Sword : AbstractMeleeWeapon {

    private Animator animator;

    protected override void Awake() {
        base.Awake();

        GameObject player = PlayerManager.instance.player;
        animator = player.GetComponent<Animator>();
        entityMovement = player.GetComponent<PlayerMovementScript>().MovementStrategy;
    }

    protected override void Start() {
        base.Start();
        damageStrategy = new PhysicalDamage(transform, weaponData.attackDamage, weaponData.attackKnockback);
    }


    public override void PlayAttackAnimation() {
        animator.SetTrigger("Sword");
    }

    public override void PlayAttackSound() {
        PlayerAudioManager.instance.sword.Play();
    }
}

