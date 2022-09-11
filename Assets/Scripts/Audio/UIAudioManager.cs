using UnityEngine;

public class UIAudioManager : MonoBehaviour {
    // Singleton
    public static UIAudioManager instance { get; private set; }
    void Awake() { instance = this; }


    // Fields
    public AudioSource dialogBlip;
    public AudioSource menuMove;
    public AudioSource menuSelect;
}
