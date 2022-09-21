using UnityEngine;



// Magic weapon is kind of weapon that detects an enemy in range and cast the spell on the target
public abstract class AbstractMagicWeapon : AbstractWeapon<MagicWeaponData> {

    [Header("References")]
    public GameObject spellPrefab;

    protected Collider2D[] overlapTargets;
    protected Collider2D range;


    protected abstract void CastSpell(Collider2D target);



    protected override void Awake() {
        base.Awake();
        range = GetComponent<Collider2D>();
    }

    protected virtual void Start() {
        overlapTargets = new Collider2D[ weaponData.targetNumber ];
    }


    public override void Attack() {
        for(int i = 0; i < weaponData.targetNumber; i++) overlapTargets[i] = null;
        range.OverlapCollider( targetContactFilter, overlapTargets );
        
        foreach (Collider2D target in overlapTargets) {
            if (target == null) continue;
            CastSpell(target);
        }
    }
}
