
using UnityEngine;
using DG.Tweening;

public class BarHealth : Health {

    [Header("HUD Configuration")]
    public GameObject healthBar;
    public GameObject healthBarBackground;
    public float tweenTime = 0.5f;


    public BarHealth() {}
    public BarHealth(float maxHealth, GameObject healthBarGroup, bool visibleOnStart) : base(maxHealth) {
        this.healthBar = healthBarGroup.transform.Find("Fill").gameObject;
        this.healthBarBackground = healthBarGroup.transform.Find("Background").gameObject;

        healthBar.SetActive(visibleOnStart);
        healthBarBackground.SetActive(visibleOnStart);
    }
    


    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        UpdateHealthBar();
    }


    public override void Heal(float amount) {
        base.Heal(amount);
        UpdateHealthBar();
    }


    public override void SetMaxHealth(float maxHealth) {
        base.SetMaxHealth(maxHealth);
        UpdateHealthBar();
    }


    protected void UpdateHealthBar() {
        healthBar.SetActive(true);
        healthBarBackground.SetActive(true);

        float healthPercentage = currentHealth / maxHealth;
        healthBar.transform.DOScaleX(healthPercentage, tweenTime).SetUpdate(true);
    }
}
