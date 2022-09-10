
using UnityEngine;



// Allows direct access to Audio sources
public class AudioManager : MonoBehaviour {

    [Header("Player sounds")]
        public AudioSource footstep;
        public AudioSource damage, punch, sword, bow, magic, death;

    [Header("Item sounds")]
        public AudioSource coin;

    [Header("Enemy sounds")]
        public AudioSource slimeImpact;



    // Accesses
    public static PlayerAudioStruct playerAudio;
    public static ItemAudioStruct itemAudio;
    public static EnemyAudioStruct enemyAudio;


    void Awake() {
        playerAudio = new PlayerAudioStruct {
            footstep = footstep,
            damage = damage,
            punch = punch,
            sword = sword,
            bow = bow,
            magic = magic,
            death = death
        };

        itemAudio = new ItemAudioStruct {
            coin = coin
        };
        
        enemyAudio = new EnemyAudioStruct {
            slimeImpact = slimeImpact
        };
    }
}
