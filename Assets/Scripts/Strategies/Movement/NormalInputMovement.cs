using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class NormalInputMovement : AbstractMovement {
    
    protected InputActionReference movementInput;
    protected InputActionReference sprintInput;
    protected Rigidbody2D rb;

    protected float walkForce = 1f;
    protected float runForce = 1f;

    protected Vector2 inputDirection;
    protected bool isSprintdown;
    

    //==============================
    // Constructors and its chain
    //==============================
    public NormalInputMovement(
        InputActionReference movementInput,
        InputActionReference sprintInput,
        Rigidbody2D rb,
        float walkForce, 
        float runForce
    ) {
        this.movementInput = movementInput;
        this.sprintInput = sprintInput;
        this.rb = rb;
        this.walkForce = walkForce;
        this.runForce = runForce;

        // Register local event listeners
        movementInput.action.performed += OnMovementChange;
        movementInput.action.canceled += OnMovementChange;
        sprintInput.action.started += OnSprintChange;
        sprintInput.action.canceled += OnSprintChange;
    }


    //==============================
    // Logic
    //==============================
    // Needs to be called in FixedUpdate()
    public override void Move() {
        if (MovementState == MovementState.MOVING) rb.AddForce(inputDirection * walkForce);
        else if (MovementState == MovementState.RUNNING) rb.AddForce(inputDirection * runForce);
    }



    void OnMovementChange(InputAction.CallbackContext ctx) {
        inputDirection = ctx.ReadValue<Vector2>();
        UpdateState();
    }


    void OnSprintChange(InputAction.CallbackContext ctx) {
        isSprintdown = ctx.ReadValueAsButton();
        UpdateState();
    }


    // Updates movement state and facing direction based on input values
    void UpdateState() {
        if (inputDirection == Vector2.zero) {
            MovementState = MovementState.IDLE;
        } else {
            MovementState = isSprintdown ? MovementState.RUNNING : MovementState.MOVING;
            FaceDirection.FacingDirection = inputDirection;
        }
    }
}
