
public interface IWeapon {
    public void OnAttackPerformed();    // Called when player presses attack button
    public void Attack();               // Called on actual animation frame where the character should really attack
    public WeaponData GetWeaponData();
}