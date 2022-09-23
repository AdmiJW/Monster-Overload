
using UnityEngine;

// ? A movement that starts off random direction, and bounces off when hitting a collider
// ! Important: You shall call movement.OnCollisionEnter2D() from the entity, or no rebounce will occur!

public class RandomContinuousMovement : AbstractMovement {

    protected Rigidbody2D chaserRigidbody;
    protected float speed;


    //==========================
    // Constructors
    //==========================
    public RandomContinuousMovement(
        GameObject entity, 
        float speed
    ) {
        this.speed = speed;
        this.chaserRigidbody = entity.GetComponent<Rigidbody2D>();

        SetRandomDirection();
        movementState = MovementState.MOVING;
    }



    //==========================
    // Logic
    //==========================
    public override void Move(float fixedDeltaTime) {
        if (!Enabled) return;
        chaserRigidbody.AddForce( faceDirection.unitVector * speed );
    }


    public override void OnCollisionEnter2D(Collision2D collision) {
        Vector2 normal = collision.GetContact(0).normal;
        faceDirection.unitVector = Vector2.Reflect( faceDirection.unitVector, normal);
    }


    protected void SetRandomDirection() {
        faceDirection.unitVector = Random.insideUnitCircle.normalized;
    }
}
