using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public class containing static utilities methods
public class Util {
    
    // Returns true if distance between pos1 and pos2 are less than range
    public static bool IsInRange(Vector3 pos1, Vector3 pos2, float range) {
        return (pos1 - pos2).sqrMagnitude <= range * range;
    }
    
}
