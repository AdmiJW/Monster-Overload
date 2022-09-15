using UnityEngine;


public class Bow : AbstractRangedWeapon {
    public override void PlayAttackAnimation() {
        playerAnimator.SetTrigger("Bow");
    }

    public override GameObject GetProjectile() {
        FaceDirection direction = player.GetComponent<PlayerMovementScript>().MovementStrategy.faceDirection;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.GetComponent<Projectile>()
            .SetWeaponData(weaponData)
            .SetImpactSound(ItemAudioManager.instance.arrowImpact)
            .OrientProjectile(direction)
            .SetTargetLayerMask(GameManager.instance.ENEMY_LAYER_MASK)
            .UsePhysicalDamageStrategy();

        return projectile;
    }
}
