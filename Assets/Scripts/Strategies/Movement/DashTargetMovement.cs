using System;
using UnityEngine;

// A movement that starts off random direction, and bounces off when hitting a collider
public class DashTargetMovement : AbstractMovement {

    private GameObject target;
    private Rigidbody2D chaserRigidbody;

    private float speed;
    private float idleDuration;
    private float warningDuration;
    private float dashDuration;

    private float countDown;



    //==========================
    // Constructors & Expansions
    //==========================
    public DashTargetMovement(
        GameObject entity,
        GameObject target,
        float speed,
        float idleDuration,
        float warningDuration,
        float dashDuration
    ) {
        this.target = target;
        this.chaserRigidbody = entity.GetComponent<Rigidbody2D>();
        this.speed = speed;
        this.idleDuration = idleDuration;
        this.warningDuration = warningDuration;
        this.dashDuration = dashDuration;

        TransitionToIdle();
    }



    //==========================
    // Logic
    //==========================
    public override void Move(float fixedDeltaTime) {
        if (!Enabled) return;

        UpdateState(fixedDeltaTime);

        if (movementState == MovementState.MOVING) {
            chaserRigidbody.AddForce(faceDirection.unitVector * speed);
        } else {
            faceDirection.unitVector = (target.transform.position - chaserRigidbody.transform.position);
        }
    }


    public override void OnCollisionEnter2D(Collision2D collision) {
        if (movementState == MovementState.MOVING) TransitionToIdle();
    }



    void UpdateState(float fixedDeltaTime) {
        countDown -= fixedDeltaTime;
        if (countDown > 0) return;

        if (movementState == MovementState.IDLE) TransitionToWarning();
        else if (movementState == MovementState.WARNING) TransitionToDashing();
        else TransitionToIdle();
    }

    void TransitionToIdle() {
        movementState = MovementState.IDLE;
        countDown = idleDuration;
    }

    void TransitionToWarning() {
        movementState = MovementState.WARNING;
        countDown = warningDuration;
    }

    void TransitionToDashing() {
        movementState = MovementState.MOVING;
        countDown = dashDuration;
    }
}
