using UnityEngine;
using DG.Tweening;


public class MusicManager : AbstractManager<MusicManager> {

    [Header("Audio Clips")]
    public AudioClip gameOver;



    private AudioSource player;


    //=========================
    //  Lifecycle
    //=========================
    protected override void Awake() {
        base.Awake();
        player = GetComponent<AudioSource>();
    }
    

    //=========================
    //  Public
    //=========================
    public void Play(AudioClip clip) {
        if (player.isPlaying) player.Stop();
        player.clip = clip;
        player.Play();
    }


    public void FadeIn(float duration = 1f) { 
        float originalVolume = player.volume;
        player.volume = 0;
        player.DOFade(originalVolume, duration).SetUpdate(true);
    }


    public void Stop() { player.Stop(); }
    public void PlayGameOver() { Play(gameOver); }
}
