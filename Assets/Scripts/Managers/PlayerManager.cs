
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Main idea:
//   This class contain all the information needed to initialize the player in every scene

public class PlayerManager : AbstractManager<PlayerManager> {

    // Contains the player in current scene, to allow others (Like chasing enemy) to access it
    [Header("Access")]
    public GameObject player;
    public GameObject healthBarGroup;
    public TMP_Text coinText;



    // These values carry across scenes. Note that these may not reflect the actual player's health and coins,
    // this is just for carrying data across scenes.
    [Header("Player Stats")]
    public float playerMaxHealth = 5;
    public float playerCurrentHealth = 5;
    public int playerCoins = 0;

    [Space]
    public List<WeaponData> playerWeapons = new List<WeaponData>();
    public WeaponType activeWeaponType;        

    [Header("Scene Initialization")]
    public Vector2 spawnPosition = new Vector2(0, 0);
    public Direction spawnFaceDirection = Direction.DOWN;


    // * TESTING USE * //
    [Header("Debug Use")]
    public WeaponData[] testWeaponData;
    
    // * TESTING USE * //
    protected override void Awake() {
        base.Awake();
        
        // * Starting weapons is for testing use.
        if (testWeaponData.Length == 0) return;
        activeWeaponType = testWeaponData[0].weaponType;
        playerWeapons.AddRange(testWeaponData);
    }

}
