
using UnityEngine;


// ! Since other scripts may want to add event listeners to faceDirection in OnEnable() which may run out of order, 
// ! move this script's execution order before default time.

[DefaultExecutionOrder(-1)]
public class PlayerMovementScript : MonoBehaviour {

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
        MovementStrategy.Move( Time.fixedDeltaTime );
    }



    //==============================
    // Logic
    //==============================
    void SetNormalInputMovementStrategy() {
        MovementStrategy = 
            new NormalInputMovement(
                InputManager.instance.player.movement,
                InputManager.instance.player.sprint,
                rb,
                walkForce, 
                runForce
            );
    }
}
