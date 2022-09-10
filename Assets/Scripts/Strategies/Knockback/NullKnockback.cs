
using UnityEngine;


// Null design pattern - No knockback
public class NullKnockback : IKnockback {
    public void Knockback(Vector2? origin = null, float knockback = 0) {}
}
