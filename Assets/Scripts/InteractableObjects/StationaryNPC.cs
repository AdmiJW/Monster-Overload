
using UnityEngine;


// An DialogInteractable with a facing direction, allowing it to face the hero when interacted
public class StationaryNPC : DialogInteractable {
    
    [Header("NPC")]
    public Direction faceDirection;

    private Rigidbody2D rb;
    private AbstractMovement movement;
    private Animator animator;


    protected override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = new NullMovement();
    }


    protected virtual void Start() {
        movement.faceDirection.onDirectionChange += OnFacingDirectionChange;
        movement.faceDirection.unitVector = faceDirection.GetVector2();
    }


   
    //==================================
    // Interactions
    //==================================
    public override void Interact(GameObject player) {
        if (dialogues.Count == 0) return;
        
        // Set npc to face player
        AbstractMovement playerMovement = player.GetComponent<PlayerMovementScript>().MovementStrategy;
        movement.faceDirection.unitVector = playerMovement.faceDirection.unitVector * -1;
        
        // On finish dialogue, set NPC back to original direction
        DialogueManager.instance.onDialogEnd += ()=> {
            movement.faceDirection.unitVector = faceDirection.GetVector2();
        };

        DialogueManager.instance.StartStory( GetStory() );
    }


    //==================================
    // Event listeners
    //==================================
    protected virtual void OnFacingDirectionChange(FaceDirection direction) {
        animator.SetFloat("Horizontal", direction.unitVector.x);
        animator.SetFloat("Vertical", direction.unitVector.y);
    }
}
