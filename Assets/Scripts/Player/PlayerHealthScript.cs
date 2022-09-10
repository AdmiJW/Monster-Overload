using System;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour, IHealth {

    [Header("Player Configuration")]
    public float maxHealth = 100f;

    public Health HealthStrategy { get; private set; }


    void Awake() {
        GameObject healthBarGroup = GameObject
            .FindWithTag("InGameUI")
            .transform
            .Find("UIHealthBar")
            .gameObject;
        HealthStrategy = new BarHealth(maxHealth, healthBarGroup, true);
    }

    void Start() {
        HealthStrategy.OnDeath += () => Time.timeScale = 0;
    }


    public void Heal(float amount) {
        HealthStrategy.Heal(amount);
    }

    public void SetMaxHealth(float maxHealth) {
        HealthStrategy.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        HealthStrategy.TakeDamage(damage);
    }

    public float GetHealth() {
        return HealthStrategy.GetHealth();
    }
}
