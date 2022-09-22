using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPool : AbstractManager<HeartPool> {

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


    
    public Dictionary<Heart, ObjectPool<GameObject>> heartPool;

    

    protected override void Awake() {
        base.Awake();

        heartPool = new Dictionary<Heart, ObjectPool<GameObject>>();

        heartPool[Heart.RED] = new ObjectPool<GameObject>(RedHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );

        heartPool[Heart.GREEN] = new ObjectPool<GameObject>(GreenHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );
        
        heartPool[Heart.GOLDEN] = new ObjectPool<GameObject>(GoldenHeartCreateFunc, true, startingHeartPoolSize)
            .SetDestroyFunc(Destroy)
            .SetGetFunc( PoolManager.SetActive )
            .SetReleaseFunc( PoolManager.SetInactive );
    }


    public void ReleaseAll() {
        foreach (ObjectPool<GameObject> heartPool in heartPool.Values ) heartPool.ReleaseAll();
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
}
