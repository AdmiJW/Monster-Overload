
using UnityEngine;
using DG.Tweening;

public class BarHealth : Health {

    [Header("HUD Configuration")]
    public GameObject healthBar;
    public GameObject healthBarBackground;
    public float tweenTime = 0.5f;


    public BarHealth() {}

    public BarHealth(
        float maxHealth, 
        GameObject healthBarGroup,
        bool visibleOnStart
    ): this(maxHealth, maxHealth, healthBarGroup, visibleOnStart) {}

    public BarHealth(
        float maxHealth, 
        float currHealth, 
        GameObject healthBarGroup, 
        bool visibleOnStart
    ): base(
        maxHealth, 
        currHealth
    ) {
        this.healthBar = healthBarGroup.transform.Find("Fill").gameObject;
        this.healthBarBackground = healthBarGroup.transform.Find("Background").gameObject;

        healthBar.SetActive(visibleOnStart);
        healthBarBackground.SetActive(visibleOnStart);

        float healthPercentage = currentHealth / maxHealth;
        healthBar.transform.localScale = new Vector3(healthPercentage, 1, 1);
    }


    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        UpdateHealthBar();
    }


    public override void Heal(float amount) {
        base.Heal(amount);
        UpdateHealthBar();
    }


    public override void SetMaxHealth(float maxHealth, bool healToFull = false) {
        base.SetMaxHealth(maxHealth);
        UpdateHealthBar();
    }


    public override void SetHealth(float health) {
        base.SetHealth(health);
        UpdateHealthBar();
    }


    public void UpdateHealthBar() {
        healthBar.SetActive(true);
        healthBarBackground.SetActive(true);
        
        float healthPercentage = currentHealth / maxHealth;
        healthBar.transform.DOScaleX(healthPercentage, tweenTime).SetUpdate(true);
    }
}
