
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour {

    public void PlayFootstep() { PlayerAudioManager.instance.footstep.Play(); }
    public void PlayDamage() { PlayerAudioManager.instance.damage.Play(); }
    public void PlayDeath() { PlayerAudioManager.instance.death.Play(); }
    
}
