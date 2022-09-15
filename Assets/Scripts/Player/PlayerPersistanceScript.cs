using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// To save/load player's data into the PlayerManager upon scene change - persisting the player's data.
[DefaultExecutionOrder(-1)]
public class PlayerPersistanceScript : MonoBehaviour {

    void Awake() {
        PlayerManager.instance.player = gameObject;
        // Spawn player at correct place
        GetComponent<Rigidbody2D>().MovePosition( PlayerManager.instance.spawnPosition );
    }


    void Start() {
        // Spawn player at correct place and correct orientation
        GetComponent<Rigidbody2D>().MovePosition( PlayerManager.instance.spawnPosition );
        GetComponent<PlayerMovementScript>()
            .MovementStrategy
            .faceDirection
            .unitVector = PlayerManager.instance.spawnFaceDirection.GetVector2(); 
    }


    public void SaveToPlayerManager() {
        // Health
        PlayerHealthScript playerHealthScript = GetComponent<PlayerHealthScript>();
        PlayerManager.instance.playerMaxHealth = playerHealthScript.healthStrategy.maxHealth;
        PlayerManager.instance.playerCurrentHealth = playerHealthScript.healthStrategy.currentHealth;

        // Coins
        PlayerManager.instance.playerCoins = GetComponent<PlayerCoinScript>().balance;

        // Weapons
        PlayerAttackScript playerAttackScript = GetComponent<PlayerAttackScript>();
        PlayerManager.instance.activeWeaponType = playerAttackScript.activeWeaponType;
        PlayerManager.instance.playerWeapons = new List<WeaponData>();

        foreach( GameObject weapon in playerAttackScript.weapons.Values ) {
            if (weapon == null) continue;
            WeaponData weaponData = weapon.GetComponent<IWeapon>().GetWeaponData();
            PlayerManager.instance.playerWeapons.Add(weaponData);
        }
    }
}
