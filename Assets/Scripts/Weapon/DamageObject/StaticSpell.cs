using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A static spell is stationary, and destroyed by animator's event.
public class StaticSpell : DamageObject {
    
    void OnAnimationEnd() {
        Destroy(gameObject);
    }
}
