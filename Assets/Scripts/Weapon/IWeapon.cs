
public interface IWeapon {
    public void OnAttackStart();    // Called when preparing for an attack
    public void OnAttackPerform();  // Called on actual animation frame where the character should really attack
    public void OnAttackEnd();      // Called when attack animation is finished

    public WeaponData GetWeaponData();
}