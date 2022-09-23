using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


// An interactable object which when interacted, display dialogues
// Contains a optional interactIndicator to give the hero a visual cue that the object is interactable, when close enough
public class DialogInteractable : MonoBehaviour, IInteractable {

    [Header("References")]
    public SpriteRenderer interactIndicator;

    [Space]
    public List<TextAsset> dialogues;


    protected AbstractRangeTrigger interactIndicatorScript;



    //==================================
    // Lifecycle
    //==================================
    protected virtual void Awake() {
        dialogues.Reverse();
        if (interactIndicator != null) interactIndicatorScript = GetComponentInChildren<LayerRangeTrigger>();
    }

    protected virtual void OnEnable() {
        if (interactIndicatorScript == null) return;
        interactIndicatorScript.RegisterEnterEvent(OnPlayerEnterInteractionZone);
        interactIndicatorScript.RegisterExitEvent(OnPlayerExitInteractionZone);
    }

    protected virtual void OnDisable() {
        if (interactIndicatorScript == null) return;
        interactIndicatorScript.UnregisterEnterEvent(OnPlayerEnterInteractionZone);
        interactIndicatorScript.UnregisterExitEvent(OnPlayerExitInteractionZone);
    }


    //==================================
    // Interactions
    //==================================
    public virtual void Interact(GameObject player) {
        if (dialogues.Count == 0) return;
        DialogueManager.instance.StartStory( GetStory() );
    }


    // Prepares a story to be passed into dialogue system, with functions binded
    protected virtual Story GetStory() {
        Story story = new Story( dialogues[ dialogues.Count - 1].text );
        BindExternalFunction(story);
        return story;
    }

    protected virtual void BindExternalFunction(Story story) {
        story.BindExternalFunction("popStory", PopStory );
    }


    protected virtual void PopStory() {
        dialogues.RemoveAt( dialogues.Count - 1 );
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
