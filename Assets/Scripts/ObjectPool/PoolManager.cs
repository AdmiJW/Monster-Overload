using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : AbstractManager<PoolManager> {

    [Header("References")]
    [SerializeField]
    private GameObject copperCoinPrefab;
    [SerializeField]
    private GameObject silverCoinPrefab;
    [SerializeField]
    private GameObject goldCoinPrefab;
    [SerializeField]
    private GameObject platinumCoinPrefab;
    
    [Header("Storage GameObject")]
    [SerializeField]
    private GameObject copperCoinParent;
    [SerializeField]
    private GameObject silverCoinParent;
    [SerializeField]
    private GameObject goldCoinParent;
    [SerializeField]
    private GameObject platinumCoinParent;


    public Dictionary<Coin, ObjectPool<GameObject>> coinPool;


    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);

        InitializeCoinPools();
    }


    //===========================================
    // Coin Pools
    //===========================================
    void InitializeCoinPools() {
        coinPool = new Dictionary<Coin, ObjectPool<GameObject>>();
        coinPool[Coin.COPPER] = new ObjectPool<GameObject>(CopperCoinCreateFunc, true, 20);
        coinPool[Coin.SILVER] = new ObjectPool<GameObject>(SilverCoinCreateFunc, true, 20);
        coinPool[Coin.GOLD] = new ObjectPool<GameObject>(GoldCoinCreateFunc, true, 20);
        coinPool[Coin.PLATINUM] = new ObjectPool<GameObject>(PlatinumCoinCreateFunc, true, 20);
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
}
