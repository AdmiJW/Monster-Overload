using UnityEngine;


public class FaceDirection {
    
    private Vector2 _facingDirection = Vector2.down;
    public Vector2 FacingDirection { 
        get { return _facingDirection; }
        set {
            if (_facingDirection.Equals(value) || value == Vector2.zero) return;
            _facingDirection = value;
            onFacingDirectionChange?.Invoke(_facingDirection);
        } 
    }


    public event System.Action<Vector2> onFacingDirectionChange;
}
