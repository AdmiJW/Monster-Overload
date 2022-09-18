
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractScript : MonoBehaviour {

    [Header("Configuration")]
    public float interactRange = 1.0f;
    
    private PlayerMovementScript playerMovement;
    private LayerMask interactableLayer;


    void Awake() {
        playerMovement = GetComponent<PlayerMovementScript>();
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    
    // Returns true if there is something interacting, else false
    public bool Interact() {
        Vector2 direction = playerMovement.MovementStrategy.faceDirection.unitVector;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, interactRange, interactableLayer);
        
        if (hit.collider != null) hit.collider.GetComponent<IInteractable>().Interact(gameObject);
        return hit.collider != null;
    }
}
