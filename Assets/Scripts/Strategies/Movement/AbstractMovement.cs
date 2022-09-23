using System;
using UnityEngine;


// Abstract movement class to represent all of the different types of movement
// Like: PlayerInput, Computed, AI, or even no movement at all.
//
// Since the game is 2D topdown, we can't really rotate the sprites. A FaceDirection class is used to represent
// the facing direction of any entities, which contain a normalized 2D vector. Defaults to Vector2.down.
// 
// It also provides a MovementState enum to represent the movement state - Run, Walk, Idle...
// 
// Event listeners are provided for
//      1. MovementState change
//      2. FacingDirection change
public abstract class AbstractMovement {

    // Whether movement will be done when Move() is called.
    public bool Enabled { get; set; } = true;
    
    public FaceDirection faceDirection;

    protected MovementState _movementState;
    public MovementState movementState {
        get { return _movementState; }
        set {
            if (_movementState == value) return;
            _movementState = value;
            onMovementStateChange?.Invoke(_movementState);
        } 
    }


    // Events
    public event Action<MovementState> onMovementStateChange;

    // Abstract methods
    public abstract void Move(float fixedDeltaTime);

    // Override when necessary
    public virtual void OnCollisionEnter2D(Collision2D collision) {}


    // Constructors
    public AbstractMovement() {
        this.faceDirection = new FaceDirection();
        this.movementState = MovementState.IDLE;
    }
}
