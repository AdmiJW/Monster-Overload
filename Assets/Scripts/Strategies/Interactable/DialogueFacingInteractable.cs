using System;
using UnityEngine;

public class DialogueFacingInteractable : AbstractFacingInteractable {
    
    public SpriteRenderer dialogueBubble;


    protected override void Awake() {
        base.Awake();
        dialogueBubble = GetComponent<SpriteRenderer>();
        interactableFacingDirection = new FaceDirection();
    }

    public override void Interact() {
        Debug.Log("interacted successfully!");
    }
}
