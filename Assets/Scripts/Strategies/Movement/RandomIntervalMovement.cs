using UnityEngine;


// ? A movement that switches between idle and moving in random direction, and bounces off when hitting a collider
// ! Important: You shall call movement.OnCollisionEnter2D() from the entity, or no rebounce will occur!

public class RandomIntervalMovement : RandomContinuousMovement {

    protected float minIdleDuration;
    protected float maxIdleDuration;
    protected float minMoveDuration;
    protected float maxMoveDuration;

    private float countDown;



    //==========================
    // Constructors & Expansions
    //==========================
    public RandomIntervalMovement(
        GameObject entity, 
        float speed,
        float minIdleDuration,
        float maxIdleDuration,
        float minMoveDuration,
        float maxMoveDuration
    ): base(entity, speed) {
        this.minIdleDuration = minIdleDuration;
        this.maxIdleDuration = maxIdleDuration;
        this.minMoveDuration = minMoveDuration;
        this.maxMoveDuration = maxMoveDuration;

        TransitionToIdle();
    }



    //==========================
    // Logic
    //==========================
    public override void Move(float fixedDeltaTime) {
        if (!Enabled) return;

        UpdateState(fixedDeltaTime);

        if (movementState != MovementState.MOVING) return;
        chaserRigidbody.AddForce( faceDirection.unitVector * speed );
    }



    void UpdateState(float fixedDeltaTime) {
        countDown -= fixedDeltaTime;
        if (countDown > 0) return;

        if (movementState == MovementState.IDLE) TransitionToMoving();
        else TransitionToIdle();
    }

    void TransitionToIdle() {
        movementState = MovementState.IDLE;
        countDown = Random.Range(minIdleDuration, maxIdleDuration);
    }

    void TransitionToMoving() {
        SetRandomDirection();
        movementState = MovementState.MOVING;
        countDown = Random.Range(minMoveDuration, maxMoveDuration);
    }
}
