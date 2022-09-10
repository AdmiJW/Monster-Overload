
using UnityEngine;

public class ChaseObjectMovement : AbstractMovement
{
    private GameObject toChase;
    private Rigidbody2D chaserRigidbody;
    
    private float speed;


    //==========================
    // Constructors
    //==========================
    public ChaseObjectMovement(
        GameObject toChase, 
        GameObject chaser, 
        float speed
    ) {
        this.toChase = toChase;
        this.speed = speed;
        this.chaserRigidbody = chaser.GetComponent<Rigidbody2D>();
    }



    //==========================
    // Logic
    //==========================
    public override void Move() {
        if (!Enabled) return;
        
        Vector2 direction = ( (Vector2)toChase.transform.position - chaserRigidbody.position).normalized;
        chaserRigidbody.AddForce(direction * speed);


        if (direction != Vector2.zero) {
            MovementState = MovementState.MOVING;
            if (FaceDirection != null) FaceDirection.FacingDirection = direction;
        } else {
            MovementState = MovementState.IDLE;
        }
    }
}
