using System;
using UnityEngine;
using NaughtyAttributes;


// Attach to a gameobject with trigger collider.
// This script will help you convey events of player entering and exit.
// Used mainly for enemy ranges
public class LayerRangeTrigger : AbstractRangeTrigger {

    [BoxGroup("Detection")]
    public LayerMask[] detectionLayers;
    private LayerMask compositeDetectionLayer = 0;


    void Awake() {
        foreach (LayerMask layer in detectionLayers) compositeDetectionLayer |= layer;
    }


    public event Action onEnter;
    public event Action onExit;


    public override void RegisterEnterEvent(Action action) { onEnter += action; }
    public override void RegisterExitEvent(Action action) { onExit += action; }
    public override void UnregisterEnterEvent(Action action) { onEnter -= action; }
    public override void UnregisterExitEvent(Action action) { onExit -= action; }


    void OnTriggerEnter2D(Collider2D other) {
        if ( (compositeDetectionLayer & 1 << other.gameObject.layer) != 0 )
            onEnter?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other) {
        if ( (compositeDetectionLayer & 1 << other.gameObject.layer) != 0 )
            onExit?.Invoke();
    }

}
