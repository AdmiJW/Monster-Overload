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


    [Header("Heart Pools")]
    [SerializeField]
    private GameObject redHeartPrefab;
    [SerializeField]
    private GameObject greenHeartPrefab;
    [SerializeField]
    private GameObject goldenHeartPrefab;
    [SerializeField]
    private GameObject redHeartParent;
    [SerializeField]
    private GameObject greenHeartParent;
    [SerializeField]
    private GameObject goldenHeartParent;
    [SerializeField]
    private int startingHeartPoolSize = 10;


    public Dictionary<Coin, ObjectPool<GameObject>> coinPool;
    public Dictionary<Heart, ObjectPool<GameObject>> heartPool;


    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);

        InitializeCoinPools();
        InitializeHeartPools();
    }


    //===================================
    // Public
    //===================================
    // Deactivates all pooled object. Use in scene transitions
    public void ReleaseAll() {
        foreach (ObjectPool<GameObject> coinPool in coinPool.Values ) coinPool.ReleaseAll();
        foreach (ObjectPool<GameObject> heartPool in heartPool.Values ) heartPool.ReleaseAll();
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
    // Heart Pools
    //===========================================
    void InitializeHeartPools() {
        heartPool = new Dictionary<Heart, ObjectPool<GameObject>>();

        heartPool[Heart.RED] = new ObjectPool<GameObject>(RedHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);

        heartPool[Heart.GREEN] = new ObjectPool<GameObject>(GreenHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);
        
        heartPool[Heart.GOLDEN] = new ObjectPool<GameObject>(GoldenHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc(SetActive)
            .SetReleaseFunc(SetInactive);
    }


    GameObject RedHeartCreateFunc() {
        GameObject heart = Instantiate(redHeartPrefab, redHeartParent.transform);
        heart.SetActive(false);
        return heart;
    }

    GameObject GreenHeartCreateFunc() {
        GameObject heart = Instantiate(greenHeartPrefab, greenHeartParent.transform);
        heart.SetActive(false);
        return heart;
    }

    GameObject GoldenHeartCreateFunc() {
        GameObject heart = Instantiate(goldenHeartPrefab, goldenHeartParent.transform);
        heart.SetActive(false);
        return heart;
    }

    //===========================================
    // GameObject functions
    //===========================================
    public static void Destroy(GameObject gameObject) {
        Destroy(gameObject);
    }

    public static void SetActive(GameObject gameObject) {
        gameObject.SetActive(true);
    }

    public static void SetInactive(GameObject gameObject) {
        gameObject.SetActive(false);
    }

}
