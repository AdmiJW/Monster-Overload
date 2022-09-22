using UnityEngine;


public class FireStaff : AbstractMagicWeapon {

    private Animator animator;

    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();

        GameObject player = PlayerManager.instance.player;
        animator = player.GetComponent<Animator>();
    }


    //===========================
    //  Logic
    //===========================
    public override void PlayAttackAnimation() {
        animator.SetTrigger("Magic");
    }

    public override void PlayAttackSound() {
        PlayerAudioManager.instance.magic.Play();
    }



    protected override void CastSpell(Collider2D target) {
        GameObject spell = Instantiate(spellPrefab, target.transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(spell.transform, weaponData.spellDamage, weaponData.spellKnockback);

        Spell s = spell.GetComponent<Spell>();

        s.IncludeTarget( GameManager.instance.ENEMY_LAYER_MASK );
        s.SetDamageStrategy(damage);
        s.SetSpellDamageSound( ItemAudioManager.instance.fire );
    }
}
