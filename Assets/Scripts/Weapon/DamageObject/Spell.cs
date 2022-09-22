using UnityEngine;
using NaughtyAttributes;


// A simple spell, deals damage only when OnSpellDamage() is called, and destroys itself 
// when OnSpellEnd() is called.
public class Spell : DamageObject {

    public int maxColliders = 1;

    [ShowIf("initializeFromInspector")]
    [BoxGroup("Initialize")]
    public AudioSource spellStartSound, spellDamageSound;


    
    protected ContactFilter2D targetContactFilter;
    protected Collider2D spellCollider;

    private Collider2D[] targetCollides;
    

    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();
        spellCollider = GetComponent<Collider2D>();
    }


    void Start() {
        targetCollides = new Collider2D[ maxColliders ];
        targetContactFilter = new ContactFilter2D();
        targetContactFilter.SetLayerMask(targetLayerMask);

        if (spellStartSound != null) spellStartSound.Play();
    }


    //===========================
    //  Public 
    //===========================
    public void SetSpellStartSound(AudioSource spellStartSound) {
        this.spellStartSound = spellStartSound;
    }

    public void SetSpellDamageSound(AudioSource spellDamageSound) {
        this.spellDamageSound = spellDamageSound;
    }

    public void SetColliderDetectionCount(int max) {
        this.maxColliders = max;
        this.targetCollides = new Collider2D[ maxColliders ];
    }


    //===========================
    //  Animation Handlers
    //===========================
    void OnSpellDamage() {
        if (spellDamageSound != null) spellDamageSound.Play();

        int hits = spellCollider.OverlapCollider(targetContactFilter, targetCollides);
        for (int i = 0; i < hits; ++i) damage.DealDamage(targetCollides[i].gameObject);
    }

    void OnSpellEnd() {
        Destroy(gameObject);
    }
}
