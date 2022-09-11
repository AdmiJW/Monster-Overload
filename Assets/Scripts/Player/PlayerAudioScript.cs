
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour {

    public void PlayFootstep() { PlayerAudioManager.instance.footstep.Play(); }
    public void PlayDamage() { PlayerAudioManager.instance.damage.Play(); }
    public void PlayDeath() { PlayerAudioManager.instance.death.Play(); }
    public void PlayPunch() { PlayerAudioManager.instance.punch.Play(); }
    public void PlaySword() { PlayerAudioManager.instance.sword.Play(); }
    public void PlayBow() { PlayerAudioManager.instance.bow.Play(); }
    public void PlayMagic() { PlayerAudioManager.instance.magic.Play(); }
    
}
