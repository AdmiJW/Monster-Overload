using UnityEngine;


public class PlayerAnimatorScript : MonoBehaviour {

    Animator animator;
    PlayerMovementScript playerMovementScript;
    PlayerHealthScript playerHealthScript;


    void Awake() {
        animator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovementScript>();
        playerHealthScript = GetComponent<PlayerHealthScript>();
    }

    void OnEnable() {
        playerMovementScript.MovementStrategy.onMovementStateChange += OnMovementStateChange;
        playerMovementScript.MovementStrategy.faceDirection.onDirectionChange += OnFacingDirectionChange;

        playerHealthScript.healthStrategy.OnDeath += OnDeath;
        playerHealthScript.healthStrategy.OnHurt += OnHurt;
    }

    void OnDisable() {
        playerMovementScript.MovementStrategy.onMovementStateChange -= OnMovementStateChange;
        playerMovementScript.MovementStrategy.faceDirection.onDirectionChange -= OnFacingDirectionChange;

        playerHealthScript.healthStrategy.OnDeath -= OnDeath;
        playerHealthScript.healthStrategy.OnHurt -= OnHurt;
    }

    //==============================
    // Handler
    //==============================
    void OnFacingDirectionChange(FaceDirection direction) {
        animator.SetFloat("Horizontal", direction.unitVector.x);
        animator.SetFloat("Vertical", direction.unitVector.y);
    }

    void OnMovementStateChange(MovementState movementState) {
        animator.SetBool("IsWalking", movementState == MovementState.MOVING);
        animator.SetBool("IsRunning", movementState == MovementState.RUNNING);
    }

    void OnDeath() {
        animator.SetTrigger("Die");
    }

    void OnHurt() {
        animator.SetTrigger("Damage");
    }
}
