using UnityEngine;


public class PlayerAnimatorScript : MonoBehaviour
{
    Animator animator;
    PlayerMovementScript playerMovementScript;
    PlayerHealthScript playerHealthScript;


    void Awake() {
        animator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovementScript>();
        playerHealthScript = GetComponent<PlayerHealthScript>();
    }

    void Start() {
        playerMovementScript.MovementStrategy.AddMovementStateListener(OnMovementStateChange);
        playerMovementScript.MovementStrategy.AddFacingDirectionListener(OnFacingDirectionChange);

        playerHealthScript.HealthStrategy.OnDeath += OnDeath;
        playerHealthScript.HealthStrategy.OnHurt += OnHurt;
    }

    //==============================
    // Handler
    //==============================
    void OnFacingDirectionChange(Vector2 direction) {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
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
