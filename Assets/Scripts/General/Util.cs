using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {
    // uses positive x direction = 0 degrees
    // returns angle 0 <= angle < 360
    public static float GetAngle(Vector2 vector) {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if (angle < 0) angle = 360f + angle;
        return angle;
    }
}
