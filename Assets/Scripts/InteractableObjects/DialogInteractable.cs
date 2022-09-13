using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogInteractable : MonoBehaviour, IInteractable {

    [Header("References")]
    public SpriteRenderer interactIndicator;

    [Space]
    public TextAsset[] dialogues;


    protected RangeTriggerScript interactIndicatorScript;
    protected Queue<TextAsset> dialoguesQueue;



    //==================================
    // Lifecycle
    //==================================
    protected virtual void Awake() {
        dialoguesQueue = new Queue<TextAsset>( dialogues );
        if (interactIndicator != null) interactIndicatorScript = GetComponentInChildren<RangeTriggerScript>();
    }

    protected virtual void OnEnable() {
        if (interactIndicatorScript == null) return;
        interactIndicatorScript.onPlayerEnter += OnPlayerEnterInteractionZone;
        interactIndicatorScript.onPlayerExit += OnPlayerExitInteractionZone;
    }

    protected virtual void OnDisable() {
        if (interactIndicatorScript == null) return;
        interactIndicatorScript.onPlayerEnter -= OnPlayerEnterInteractionZone;
        interactIndicatorScript.onPlayerExit -= OnPlayerExitInteractionZone;
    }


    //==================================
    // Interactions
    //==================================
    public virtual void Interact(GameObject player) {
        if (dialoguesQueue.Count == 0) return;
        DialogueManager.instance.StartStory( GetStory() );
    }


    // Prepares a story to be passed into dialogue system, with functions binded
    protected virtual Story GetStory() {
        Story story = new Story(dialoguesQueue.Peek().text);
        BindExternalFunction(story);
        return story;
    }

    protected virtual void BindExternalFunction(Story story) {
        story.BindExternalFunction("popStory", PopStory );
    }


    protected virtual void PopStory() {
        dialoguesQueue.Dequeue();
    }


    //==================================
    // Event listeners
    //==================================
    void OnPlayerEnterInteractionZone() {
        interactIndicator.enabled = true; 
    }

    void OnPlayerExitInteractionZone() {
        interactIndicator.enabled = false;
    }
}
