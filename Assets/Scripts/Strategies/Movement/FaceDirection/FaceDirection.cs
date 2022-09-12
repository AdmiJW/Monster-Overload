using UnityEngine;


// Since our game is 2D top-down, we can't really rotate the sprites.
// Therefore, use this class to represent the facing direction of any entities
// The Vector2 facing direction is always normalized.
public class FaceDirection {
    // Private fields
    private Vector2 _direction;

    // Public properties
    public Vector2 direction { 
        get { return _direction; }
        set {
            if ( _direction.Equals(value) || value == Vector2.zero) return;
            _direction = value.normalized;
            onDirectionChange?.Invoke(_direction);
        } 
    }

    // Events
    public event System.Action<Vector2> onDirectionChange;



    // Constructor
    public FaceDirection(): this(Vector2.down) {}

    public FaceDirection(Vector2 direction) {
        this.direction = direction;
    }
}
