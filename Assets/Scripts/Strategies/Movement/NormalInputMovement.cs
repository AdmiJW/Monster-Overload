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
    ): base() {
        this.movementInput = movementInput;
        this.sprintInput = sprintInput;
        this.rb = rb;
        this.walkForce = walkForce;
        this.runForce = runForce;

        // Register local event listeners
        movementInput.action.performed += OnMovementChange;
        movementInput.action.canceled += OnMovementChange;
        sprintInput.action.started += OnSprintButtonChange;
        sprintInput.action.canceled += OnSprintButtonChange;
    }


    //==============================
    // Logic
    //==============================
    // Needs to be called in FixedUpdate()
    public override void Move(float fixedDeltaTime) {
        if (movementState == MovementState.MOVING) rb.AddForce(inputDirection * walkForce);
        else if (movementState == MovementState.RUNNING) rb.AddForce(inputDirection * runForce);
    }



    void OnMovementChange(InputAction.CallbackContext ctx) {
        inputDirection = ctx.ReadValue<Vector2>();
        UpdateState();
    }


    void OnSprintButtonChange(InputAction.CallbackContext ctx) {
        isSprintdown = ctx.ReadValueAsButton();
        UpdateState();
    }


    // Updates movement state and facing direction based on input values
    void UpdateState() {
        if (inputDirection == Vector2.zero) movementState = MovementState.IDLE;
        else {
            movementState = (isSprintdown ? MovementState.RUNNING : MovementState.MOVING);
            faceDirection.unitVector = inputDirection;
        }
    }
}
