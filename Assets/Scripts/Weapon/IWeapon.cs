
public interface IWeapon {
    public void OnAttackPerformed();    // Called when preparing for an attack
    public void Attack();               // Called on actual animation frame where the character should really attack
    public WeaponData GetWeaponData();
}