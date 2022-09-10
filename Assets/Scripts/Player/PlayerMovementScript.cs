using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour {

    [Header("References")]
    public InputActionReference movementInput;
    public InputActionReference sprintInput;

    [Header("Movement Settings")]
    public float walkForce = 1f;
    public float runForce = 1f;

    private Rigidbody2D rb;
    public AbstractMovement MovementStrategy { get; private set; }

    //==============================
    // Lifecycle
    //==============================
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        SetNormalInputMovementStrategy();
    }

    void FixedUpdate() {
        MovementStrategy.Move();
    }



    //==============================
    // Logic
    //==============================
    void SetNormalInputMovementStrategy() {
        MovementStrategy = 
            new NormalInputMovement(
                movementInput, 
                sprintInput,
                rb,
                walkForce, 
                runForce
            )
            .WithFaceDirection();
    }
}
