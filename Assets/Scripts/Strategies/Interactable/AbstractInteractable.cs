using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbstractInteractable: MonoBehaviour {

    [Header("Input Reference")]
    public InputActionReference interactButton;


    protected Collider2D triggerZone;

    //============
    // State
    //============
    
    // Interactable flag - indicates the player can interact with this object when interactButton is pressed.
    // Maybe when a dialogue balloon (...) pop up? 
    private bool _isInteractable = false;


    // Public properties
    public bool isInteractable {
        get { return _isInteractable; }
        set {
            if (_isInteractable == value) return;
            _isInteractable = value;
            onInteractableStatehange?.Invoke(_isInteractable);
        }
    }


    // Events
    public event Action<bool> onInteractableStatehange;


    //=====================
    // Abstract method
    //=====================
    public abstract void Interact();
    public abstract void OnTriggerEnter2D(Collider2D other);
    public abstract void OnTriggerExit2D(Collider2D other);


    //=====================
    // Listener
    //=====================
    protected virtual void OnInteractButtonPress(InputAction.CallbackContext context) {
        if (isInteractable) Interact();
    }


    //=====================
    // Lifecycle
    //=====================
    protected virtual void Awake() {
        triggerZone = GetComponent<Collider2D>();
    }

    protected virtual void OnEnable() {
        interactButton.action.performed += OnInteractButtonPress;
    }

    protected virtual void OnDisable() {
        interactButton.action.performed -= OnInteractButtonPress;
    }
}
