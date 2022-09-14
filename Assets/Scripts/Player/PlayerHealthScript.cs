using UnityEngine;


// The design is to move the healthStrategy to the singleton, so player health is redirected to there.
public class PlayerHealthScript : MonoBehaviour, IHealth {

    [Header("Reference")]
    public BarHealth healthStrategy;


    void Awake() {
        if (PlayerManager.instance.healthBarGroup == null) return;

        healthStrategy = new BarHealth(
            PlayerManager.instance.playerMaxHealth,
            PlayerManager.instance.playerCurrentHealth,
            PlayerManager.instance.healthBarGroup,
            true
        );
    }


    public void Heal(float amount) {
        healthStrategy.Heal(amount);
    }

    public void SetMaxHealth(float maxHealth, bool healToFull = false) {
        healthStrategy.SetMaxHealth(maxHealth, healToFull);
    }

    public void TakeDamage(float damage) {
        healthStrategy.TakeDamage(damage);
    }

    public float GetHealth() {
        return healthStrategy.GetHealth();
    }

    public void SetHealth(float health) {
        healthStrategy.SetHealth(health);
    }
}
