using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THis kind of interactable requires player to face the interactable before being able to interact with it.
public abstract class AbstractFacingInteractable : AbstractInteractable {

    // You may want this initialized from parent's AbstractMovement class.
    public FaceDirection interactableFacingDirection;


    
    public override void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        AbstractMovement playerMovement = other.GetComponent<PlayerMovementScript>().MovementStrategy;
        playerMovement.faceDirection.onDirectionChange += OnPlayerFacingDirectionChange;
        OnPlayerFacingDirectionChange( playerMovement.faceDirection.direction );
    }


    public override void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        isInteractable = false;
        other.gameObject
            .GetComponent<PlayerMovementScript>()
            .MovementStrategy
            .faceDirection
            .onDirectionChange -= OnPlayerFacingDirectionChange;
    }


    protected virtual void OnPlayerFacingDirectionChange(Vector2 playerFacingDirection) {
        // Dot product of 2 NORMALIZED vector is -1 if opposite direction.
        isInteractable = (Vector2.Dot(playerFacingDirection, interactableFacingDirection.direction) == -1);
    }

}
