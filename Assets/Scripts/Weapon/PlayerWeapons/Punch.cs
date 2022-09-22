using UnityEngine;


public class Punch : AbstractMeleeWeapon {

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
        animator.SetTrigger("Punch");
    }

    public override void PlayAttackSound() {
        PlayerAudioManager.instance.punch.Play();
    }
}

