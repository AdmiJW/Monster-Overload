using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Invulnerable for set period of time, then deactivates invulnerability.
public class TimedInvulnerable : IInvulnerable {

    MonoBehaviour entity;
    SpriteRenderer spriteRenderer;
    float flashTime;
    float duration;
    bool isInvulnerable = false;

    IEnumerator invulnerabilityCoroutine;


    //================================
    // Constructor & Chain
    //================================
    public TimedInvulnerable(float duration) {
        this.duration = duration;
    }

    // I needed the StartCoroutine() from MonoBehavior so...
    public TimedInvulnerable WithSpriteFlashing(MonoBehaviour entity, float flashTime) {
        this.entity = entity;
        this.spriteRenderer = entity.GetComponent<SpriteRenderer>();
        this.flashTime = flashTime;
        return this;
    }


    //=====================================================
    // Public 
    //=====================================================
    public void ActivateVulnerable() {
        if (invulnerabilityCoroutine != null) entity.StopCoroutine(invulnerabilityCoroutine);
        invulnerabilityCoroutine = StartInvulnerabilityFrame();
        entity.StartCoroutine(invulnerabilityCoroutine);
    }


    bool IInvulnerable.IsInvulnerable() {
        return isInvulnerable;
    }



    //=====================================================
    // Coroutines
    //=====================================================
    IEnumerator StartInvulnerabilityFrame() {
        isInvulnerable = true;

        // Flashing the sprite renderer (if is set)
        if (spriteRenderer != null) {
            for (float t = duration / flashTime; t > 0; t -= 1) {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(flashTime);
            }
            spriteRenderer.enabled = true;
        }

        isInvulnerable = false;
    }
}
