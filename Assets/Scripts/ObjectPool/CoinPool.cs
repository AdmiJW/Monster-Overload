using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : AbstractManager<CoinPool> {

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

        coinPool = new Dictionary<Coin, ObjectPool<GameObject>>();

        coinPool[Coin.COPPER] = new ObjectPool<GameObject>(CopperCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );

        coinPool[Coin.SILVER] = new ObjectPool<GameObject>(SilverCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );
        
        coinPool[Coin.GOLD] = new ObjectPool<GameObject>(GoldCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );
        
        coinPool[Coin.PLATINUM] = new ObjectPool<GameObject>(PlatinumCoinCreateFunc, true, startingCoinPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );
    }


    public void ReleaseAll() {
        foreach (ObjectPool<GameObject> coinPool in coinPool.Values ) coinPool.ReleaseAll();
    }



    // Pool object creation function
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
