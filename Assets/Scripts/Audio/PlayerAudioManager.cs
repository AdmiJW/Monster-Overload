using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    // Singleton
    public static PlayerAudioManager instance { get; private set; }
    void Awake() { instance = this; }


    // Fields
    public AudioSource footstep;
    public AudioSource damage;
    public AudioSource punch;
    public AudioSource sword;
    public AudioSource bow;
    public AudioSource magic;
    public AudioSource death;
}
