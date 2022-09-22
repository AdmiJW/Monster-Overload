using System;
using UnityEngine.InputSystem;


[Serializable]
public struct PlayerControlStruct {
    [NonSerialized]
    public InputActionMap actionMap;

    public InputActionReference movement;
    public InputActionReference attackOrInteract;
    public InputActionReference sprint;

    // Weapons
    public InputActionReference switchNext;
    public InputActionReference switchPrevious;
    public InputActionReference switchPunch;
    public InputActionReference switchSword;
    public InputActionReference switchBow;
    public InputActionReference switchFireStaff;
}