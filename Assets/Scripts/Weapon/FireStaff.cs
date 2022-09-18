using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStaff : AbstractMagicWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Magic");
    }



    protected override void CastSpell(Collider2D target) {
        GameObject spell = Instantiate(spellPrefab, target.transform.position, Quaternion.identity);
        IDamage damage = new PhysicalDamage(spell.transform, weaponData.spellDamage, weaponData.spellKnockback);
        StaticSpell s = spell.GetComponent<StaticSpell>();
        s.IncludeTarget( GameManager.instance.ENEMY_LAYER_MASK );
        s.SetDamageStrategy(damage);
    }
}
