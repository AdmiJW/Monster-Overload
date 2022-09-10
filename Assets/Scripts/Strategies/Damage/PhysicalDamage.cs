using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDamage : IDamage {

    Transform source;
    float damage;
    float knockback;
    bool ignoreInvulnerability = false;
    bool active = true;


    //=====================================================
    // Constructor & Chain
    //=====================================================
    public PhysicalDamage(Transform source, float damage, float knockback) {
        this.damage = damage;
        this.knockback = knockback;
        this.source = source;
    }

    public PhysicalDamage IgnoreInvulnerable() {
        this.ignoreInvulnerability = true;
        return this;
    }



    //=====================================================
    // Public
    //=====================================================
    public void DealDamage(GameObject target) {
        if (!active) return;

        IInvulnerable invulnerableComp = target.GetComponent<IInvulnerable>();
        if (ignoreInvulnerability || invulnerableComp.IsInvulnerable()) return;

        IHealth healthComp = target.GetComponent<IHealth>();
        IKnockback knockbackComp = target.GetComponent<IKnockback>();
        healthComp?.TakeDamage(damage);
        knockbackComp?.Knockback(source.position, knockback);
        if (healthComp.GetHealth() != 0) invulnerableComp?.ActivateVulnerable();
    }


    public void SetActive(bool active) {
        this.active = active;
    }
}
