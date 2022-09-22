using UnityEngine;
using NaughtyAttributes;


public class RedNinjaWeapon : AbstractMagicWeapon {
    
    [BoxGroup("References")]
    public Animator animator;


    protected override void CastSpell(Collider2D target) {
        GameObject spellObject = Instantiate(spellPrefab, target.transform.position, Quaternion.identity);
        Spell spell = spellObject.GetComponent<Spell>();
        IDamage damage = new PhysicalDamage(spell.transform, weaponData.spellDamage, weaponData.spellKnockback);

        spell.IncludeTarget( GameManager.instance.PLAYER_LAYER_MASK );        
        spell.SetDamageStrategy(damage);
        spell.SetSpellStartSound( ItemAudioManager.instance.spellCharging );
        spell.SetSpellDamageSound( ItemAudioManager.instance.heavyImpact );
    }



    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }


    public override void PlayAttackSound() {}
}
