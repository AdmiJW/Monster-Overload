using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : AbstractManager<PoolManager> {

    [Header("Coin Pools")]
    [SerializeField]
    private GameObject copperCoinPrefab;
    [SerializeField]
    private GameObject silverCoinPrefab;
    [SerializeField]
    private GameObject goldCoinPrefab;
    [SerializeField]
    private GameObject platinumCoinPrefab;
    [SerializeField]
    private GameObject copperCoinParent;
    [SerializeField]
    private GameObject silverCoinParent;
    [SerializeField]
    private GameObject goldCoinParent;
    [SerializeField]
    private GameObject platinumCoinParent;
    [SerializeField]
    private int startingCoinPoolSize = 20;



    public Dictionary<Coin, ObjectPool<GameObject>> coinPool;


    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);

        InitializeCoinPools();
    }


    //===================================
    // Public
    //===================================
    // Deactivates all pooled object. Use in scene transitions
    public void ReleaseAll() {
        foreach (ObjectPool<GameObject> coinPool in coinPool.Values ) coinPool.ReleaseAll();
    }

    //===========================================
    // Coin Pools
    //===========================================
    void InitializeCoinPools() {
        coinPool = new Dictionary<Coin, ObjectPool<GameObject>>();

        coinPool[Coin.COPPER] = new ObjectPool<GameObject>(CopperCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);

        coinPool[Coin.SILVER] = new ObjectPool<GameObject>(SilverCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);
        
        coinPool[Coin.GOLD] = new ObjectPool<GameObject>(GoldCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);
        
        coinPool[Coin.PLATINUM] = new ObjectPool<GameObject>(PlatinumCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);
    }


    GameObject CopperCoinCreateFunc() {
        GameObject coin = Instantiate(copperCoinPrefab, copperCoinParent.transform);
        coin.SetActive(false);
        return coin;
    }

    GameObject SilverCoinCreateFunc() {
        GameObject coin = Instantiate(silverCoinPrefab, silverCoinParent.transform);
        coin.SetActive(false);
        return coin;
    }

    GameObject GoldCoinCreateFunc() {
        GameObject coin = Instantiate(goldCoinPrefab, goldCoinParent.transform);
        coin.SetActive(false);
        return coin;
    }

    GameObject PlatinumCoinCreateFunc() {
        GameObject coin = Instantiate(platinumCoinPrefab, platinumCoinParent.transform);
        coin.SetActive(false);
        return coin;
    }



    //===========================================
    // GameObject functions
    //===========================================
    public void Destroy(GameObject gameObject) {
        Destroy(gameObject);
    }

    public void SetActive(GameObject gameObject) {
        gameObject.SetActive(true);
    }

    public void SetInactive(GameObject gameObject) {
        gameObject.SetActive(false);
    }

}
