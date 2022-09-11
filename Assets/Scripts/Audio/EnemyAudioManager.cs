using UnityEngine;


public class EnemyAudioManager : MonoBehaviour {
    // Singleton
    public static EnemyAudioManager instance { get; private set; }
    void Awake() { instance = this; }


    // Fields
    public AudioSource slimeImpact;
}
