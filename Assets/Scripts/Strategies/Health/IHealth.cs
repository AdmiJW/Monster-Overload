


// Only used for interfacing for the enemy to be used in GetComponent<>.
// If you want to use this as datatype, consider using Health instead
public interface IHealth {
    public void TakeDamage(float damage);
    public void Heal(float amount);
    public void SetMaxHealth(float maxHealth);
    public float GetHealth();
}
