using System;
using UnityEngine;

// A movement that starts off random direction, and bounces off when hitting a collider
public class DashTargetMovement : AbstractMovement {

    // Enum for local use
    public enum DashTargetMovementState {
        IDLE,
        WARNING,
        DASHING
    };


    private GameObject target;
    private Rigidbody2D chaserRigidbody;

    private float speed;
    private float idleDuration;
    private float warningDuration;
    private float dashDuration;

    private DashTargetMovementState state;
    private float countDown;


    public event Action<DashTargetMovementState> onStateChange;



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

    public DashTargetMovement AddStateListener(Action<DashTargetMovementState> listener) {
        onStateChange += listener;
        return this;
    }

    public DashTargetMovement RemoveStateListener(Action<DashTargetMovementState> listener) {
        onStateChange -= listener;
        return this;
    }



    //==========================
    // Logic
    //==========================
    public override void Move(float fixedDeltaTime) {
        if (!Enabled) return;

        UpdateState(fixedDeltaTime);

        if (state == DashTargetMovementState.DASHING) {
            chaserRigidbody.AddForce(faceDirection.unitVector * speed);
        } else {
            faceDirection.unitVector = (target.transform.position - chaserRigidbody.transform.position);
        }
    }


    public override void OnCollisionEnter2D(Collision2D collision) {
        if (state == DashTargetMovementState.DASHING) TransitionToIdle();
    }



    void UpdateState(float fixedDeltaTime) {
        countDown -= fixedDeltaTime;
        if (countDown > 0) return;

        if (state == DashTargetMovementState.IDLE) TransitionToWarning();
        else if (state == DashTargetMovementState.WARNING) TransitionToDashing();
        else TransitionToIdle();
    }

    void TransitionToIdle() {
        state = DashTargetMovementState.IDLE;
        movementState = MovementState.IDLE;
        countDown = idleDuration;
        onStateChange?.Invoke(state);
    }

    void TransitionToWarning() {
        state = DashTargetMovementState.WARNING;
        countDown = warningDuration;
        onStateChange?.Invoke(state);
    }

    void TransitionToDashing() {
        state = DashTargetMovementState.DASHING;
        movementState = MovementState.MOVING;
        countDown = dashDuration;
        onStateChange?.Invoke(state);
    }
}
