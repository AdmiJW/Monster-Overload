using UnityEngine;

public class ItemAudioManager : MonoBehaviour {
    // Singleton
    public static ItemAudioManager instance { get; private set; }
    void Awake() { instance = this; }


    // Fields
    public AudioSource coin;
}
