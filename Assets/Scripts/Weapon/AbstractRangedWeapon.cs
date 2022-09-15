using UnityEngine;


public abstract class AbstractRangedWeapon : AbstractWeapon<RangedWeaponData> {

    [Header("References")]
    public GameObject projectilePrefab;


    //===========================
    // Logic
    //===========================
    public override void Attack() {
        // Get player's direction
        Vector2 direction = player.GetComponent<PlayerMovementScript>().MovementStrategy.faceDirection.direction;
        Debug.Log("Shoot at direction: " + direction);
    }
}
