using System;
using UnityEngine;


// Attach to a gameobject with trigger collider.
// This script will help you convey events of player entering and exit.
// Used mainly for enemy ranges
public class RangeTriggerScript : MonoBehaviour {

    public event Action onPlayerEnter;
    public event Action onPlayerExit;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) onPlayerEnter?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) onPlayerExit?.Invoke();
    }
}
