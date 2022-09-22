using System;
using UnityEngine.InputSystem;


[Serializable]
public struct MenuControlStruct {
    [NonSerialized]
    public InputActionMap actionMap;

    public InputActionReference up;
    public InputActionReference down;
    public InputActionReference left;
    public InputActionReference right;
    public InputActionReference select;
    public InputActionReference cancel;
    public InputActionReference respawn;
}