
using UnityEngine;

public class AddForceKnockback : IKnockback {
    private Rigidbody2D rb;
    private float knockbackResistance;


    public AddForceKnockback(Rigidbody2D rb, float knockbackResistance) {
        this.rb = rb;
        this.knockbackResistance = knockbackResistance;
    }


    public void Knockback(Vector2? origin = null, float knockback = 0) {
        Vector2 direction = ( rb.position - (Vector2)origin ).normalized;
        rb.AddForce(direction * (Mathf.Max(0, knockback - knockbackResistance)), ForceMode2D.Impulse);
    }
}
