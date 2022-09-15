using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


// Main idea:
//   This class contain all the information needed to initialize the player in every scene

public class PlayerManager : AbstractManager<PlayerManager> {

    // Contains the player in current scene, to allow others (Like chasing enemy) to access it
    [Header("Access")]
    public GameObject player;
    public GameObject healthBarGroup;
    public TMP_Text coinText;
    public TMP_Text statusText;



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


    [Header("Debug Use")]
    public WeaponData[] testWeaponData;
    

    private IEnumerator fadeStatusTextCoroutine = null;


    //===========================
    //  Lifecycle
    //===========================
    protected override void Awake() {
        base.Awake();
        
        if (testWeaponData.Length == 0) return;
        Debug.LogWarning("PlayerManager: Test weapon data is not empty. This is for testing use only.");
        activeWeaponType = testWeaponData[0].weaponType;
        playerWeapons.AddRange(testWeaponData);
    }



    //===========================
    //  Public
    //===========================
    public void ShowStatusText(string text, float duration = 2f) {
        statusText.alpha = 1;
        statusText.text = text;
        if (fadeStatusTextCoroutine != null) StopCoroutine(fadeStatusTextCoroutine);
        statusText.DOKill();
        fadeStatusTextCoroutine = HideStatusTextCoroutine(duration);
        StartCoroutine(fadeStatusTextCoroutine);
    }


    //===========================
    //  Coroutine
    //===========================
    IEnumerator HideStatusTextCoroutine(float delay) {
        yield return new WaitForSecondsRealtime(delay);
        statusText.DOFade(0, 1f);
        fadeStatusTextCoroutine = null;
    }
}
