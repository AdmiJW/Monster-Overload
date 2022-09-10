
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour {

    public void PlayFootstep() { AudioManager.playerAudio.footstep.Play(); }
    public void PlayDamage() { AudioManager.playerAudio.damage.Play(); }
    public void PlayDeath() { AudioManager.playerAudio.death.Play(); }
    public void PlayPunch() { AudioManager.playerAudio.punch.Play(); }
    public void PlaySword() { AudioManager.playerAudio.sword.Play(); }
    public void PlayBow() { AudioManager.playerAudio.bow.Play(); }
    public void PlayMagic() { AudioManager.playerAudio.magic.Play(); }
    
}
