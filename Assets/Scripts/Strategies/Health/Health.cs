using System;
using UnityEngine;


public class Health : IHealth {

    public float maxHealth { get; protected set; } = 100f;
    public float currentHealth { get; protected set; }

    public event Action OnHurt;
    public event Action OnDeath;
    public event Action OnHeal;



    public Health() {}
    public Health(float maxHealth): this(maxHealth, maxHealth) {}
    public Health(float maxHealth, float currHealth) {
        this.maxHealth = maxHealth;
        this.currentHealth = currHealth;
    }


    //==============================
    //  Public Methods
    //==============================
    public virtual void TakeDamage(float damage) {
        if (damage == 0) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (currentHealth == 0) OnDeath?.Invoke();
        else OnHurt?.Invoke();
    }

    public virtual void Heal(float amount) {
        if (amount == 0) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHeal?.Invoke();
    }

    public virtual void SetHealth(float health) {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }

    public virtual void SetMaxHealth(float maxHealth, bool healToFull = false) {
        this.maxHealth = maxHealth;
        if (healToFull) currentHealth = maxHealth;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    public float GetHealth() {
        return currentHealth;
    }
}
