
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
    public int activeWeaponIndex = 0;        // So that player won't suddenly switch to another weapon

    [Header("Scene Initialization")]
    public Vector2 spawnPosition = new Vector2(0, 0);
    public Direction spawnFaceDirection = Direction.DOWN;



    [Header("Debug Use")]
    public List<GameObject> startingWeapons = new List<GameObject>();
    

    protected override void Awake() {
        base.Awake();
        
        foreach(GameObject weapon in startingWeapons) {
            playerWeapons.Add(weapon.GetComponent<AbstractWeapon>().GetWeaponData());
        }
    }

}
