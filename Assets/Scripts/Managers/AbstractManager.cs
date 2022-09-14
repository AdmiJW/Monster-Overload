
using UnityEngine;


// All manangers are singletons, and the script shall be initialized before any other scripts
[DefaultExecutionOrder(-100)]
public abstract class AbstractManager<T>: MonoBehaviour where T: AbstractManager<T> {

    public static T instance { get; private set; }

    protected virtual void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = (T)this;
    }
}
