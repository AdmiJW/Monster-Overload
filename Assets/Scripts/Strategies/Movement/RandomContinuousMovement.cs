
using UnityEngine;

// A movement that starts off random direction, and bounces off when hitting a collider
public class RandomContinuousMovement : AbstractMovement {

    private Rigidbody2D chaserRigidbody;
    private float speed;
    private Vector2 direction;


    //==========================
    // Constructors
    //==========================
    public RandomContinuousMovement(
        GameObject entity, 
        float speed
    ) {
        this.speed = speed;
        this.chaserRigidbody = entity.GetComponent<Rigidbody2D>();

        // Start off in random direction.
        direction = Random.insideUnitCircle.normalized;
        faceDirection.unitVector = direction;
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
        direction = Vector2.Reflect(direction, normal);
        faceDirection.unitVector = direction;
    }
}
