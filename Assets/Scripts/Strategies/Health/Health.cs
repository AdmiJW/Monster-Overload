using System;
using UnityEngine;


public class Health : IHealth {

    protected float maxHealth = 100f;
    protected float currentHealth;

    public event Action OnHurt;
    public event Action OnDeath;
    public event Action OnHeal;



    public Health() {}
    
    public Health(float maxHealth) {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
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

    public virtual void SetMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public float GetHealth() {
        return currentHealth;
    }
}
