using System;
using System.Collections.Generic;
using UnityEngine;

// Abstract movement class to represent all of the different types of movement
// Like: PlayerInput, Computed, AI, or even no movement at all.
//
// It provides a FaceDirection composition to see whether if the movement affects the facing direction
// If no faceDirection functionality is needed, simply put null.
// 
// It also provides a MovementState enum to represent the movement state - Run, Walk, Idle...
// Event listeners are provided aswell
public abstract class AbstractMovement {

    public bool Enabled { get; set; } = true;
    public FaceDirection FaceDirection { get; private set; } = null;
    protected MovementState _movementState = MovementState.IDLE;
    public MovementState MovementState { 
        get { return _movementState; }
        set {
            if (_movementState == value) return;
            _movementState = value;
            onMovementStateChange?.Invoke(_movementState);
        } 
    }

    protected event Action<MovementState> onMovementStateChange;

    public abstract void Move();

    //==============================
    // FaceDirection Methods
    //==============================
    // This method shall be chained on constructor, to provide flexibility to use faceDirection or not
    // Like:
    //      new AbstractMovement()
    //          .WithFaceDirection(...);
    public AbstractMovement WithFaceDirection() {
        FaceDirection = new FaceDirection();
        return this;
    }

    public AbstractMovement AddFacingDirectionListener(Action<Vector2> listener) {
        FaceDirection.onFacingDirectionChange += listener;
        return this;
    }

    public AbstractMovement RemoveFacingDirectionListener(Action<Vector2> listener) {
        FaceDirection.onFacingDirectionChange -= listener;
        return this;
    }


    //==============================
    // MovementState Methods
    //==============================
    public AbstractMovement AddMovementStateListener(Action<MovementState> listener) {
        onMovementStateChange += listener;
        return this;
    }

    public AbstractMovement RemoveMovementStateListener(Action<MovementState> listener) {
        onMovementStateChange -= listener;
        return this;
    }
}
