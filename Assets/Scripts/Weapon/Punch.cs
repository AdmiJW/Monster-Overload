using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : AbstractMeleeWeapon {

    public override void PlayAttackAnimation() {
        animator.SetTrigger("Punch");
    }
    
}
