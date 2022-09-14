using UnityEngine;

public class UIAudioManager : MonoBehaviour {
    // Fields
    public AudioSource dialogBlip;
    public AudioSource menuMove;
    public AudioSource menuSelect;
    
    
    // Singleton
    public static UIAudioManager instance { get; private set; }
    
    void Awake() { 
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }

        instance = this; 
    }
}
