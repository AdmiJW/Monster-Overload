using System;
using UnityEngine;

public abstract class AbstractRangeTrigger : MonoBehaviour {
    
    public abstract void RegisterEnterEvent(Action action);
    public abstract void RegisterExitEvent(Action action);
    public abstract void UnregisterEnterEvent(Action action);
    public abstract void UnregisterExitEvent(Action action);

}
