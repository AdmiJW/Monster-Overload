using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : AbstractManager<PoolManager> {

    [Header("Pools")]
    public CoinPool coinPoolScript;
    public HeartPool heartPoolScript;

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    //===================================
    // Public
    //===================================
    // Deactivates all pooled object. Use in scene transitions
    public void ReleaseAll() {
        coinPoolScript.ReleaseAll();
        heartPoolScript.ReleaseAll();
    }

    //===========================================
    // GameObject functions
    //===========================================
    public static void SetActive(GameObject gameObject) {
        gameObject.SetActive(true);
    }

    public static void SetInactive(GameObject gameObject) {
        gameObject.SetActive(false);
    }

}
