using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackScript : MonoBehaviour, IKnockback {

    [Header("Knockback Configuration")]
    public float knockbackResistance = 1f;

    IKnockback knockbackStrategy;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        SetAddForceKnockbackStrategy();
    }

    void SetAddForceKnockbackStrategy() {
        knockbackStrategy = new AddForceKnockback(rb, knockbackResistance);
    }



    public void Knockback(Vector2? origin = null, float knockback = 0) {
        knockbackStrategy.Knockback(origin, knockback);
    }
}
