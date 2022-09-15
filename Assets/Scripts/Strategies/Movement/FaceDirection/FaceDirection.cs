using UnityEngine;


// Since our game is 2D top-down, we can't really rotate the sprites.
// Therefore, use this class to represent the facing direction of any entities
// The Vector2 facing direction is always normalized.
public class FaceDirection {
    // Private fields
    private Vector2 _unitVector;

    // Public properties
    public Vector2 unitVector { 
        get { return _unitVector; }
        set {
            if ( _unitVector.Equals(value) || value == Vector2.zero) return;
            _unitVector = value.normalized;
            onDirectionChange?.Invoke(this);
        }
    }

    // Events
    public event System.Action<FaceDirection> onDirectionChange;


    // Constructor
    public FaceDirection(): this(Vector2.down) {}

    public FaceDirection(Vector2 direction) {
        this.unitVector = direction;
    }


    // Public methods
    public float GetAngle() {
        // uses positive x direction = 0 degrees
        float angle = Mathf.Atan2(unitVector.y, unitVector.x) * Mathf.Rad2Deg;
        if (angle < 0) angle = 360f + angle;
        return angle;
    }
}
