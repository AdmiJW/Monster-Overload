using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEmitter: MonoBehaviour {

    [Header("References")]
    public GameObject copperCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject goldCoinPrefab;
    public GameObject platinumCoinPrefab;


    [Header("Monster Drop")]
    public int minCoinDrop = 1;
    public int maxCoinDrop = 1;
    
    public float minInitialForce = 20f;
    public float maxInitialForce = 20f;
    
    


    public void Activate() {
        int dropAmount = Random.Range(minCoinDrop, maxCoinDrop);
        GameObject[] coins = GetCoinDrops(dropAmount);

        // Apply force to each coin
        foreach (GameObject coin in coins) {
            coin.SetActive(true);

            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            rb.MovePosition(transform.position);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float initialForce = Random.Range(minInitialForce, maxInitialForce);
            rb.AddForce(randomDirection * initialForce, ForceMode2D.Impulse);
        }
    }


    GameObject[] GetCoinDrops(int value) {
        int platinumCount = value / Coin.PLATINUM.GetCoinValue();
        value -= platinumCount * Coin.PLATINUM.GetCoinValue();
        int goldCount = value / Coin.GOLD.GetCoinValue();
        value -= goldCount * Coin.GOLD.GetCoinValue();
        int silverCount = value / Coin.SILVER.GetCoinValue();
        value -= silverCount * Coin.SILVER.GetCoinValue();
        int copperCount = value / Coin.COPPER.GetCoinValue();
        value -= copperCount * Coin.COPPER.GetCoinValue();

        GameObject[] coins = new GameObject[platinumCount + goldCount + silverCount + copperCount];
        int index = 0;
        for (int i = 0; i < platinumCount; i++) coins[index++] = PoolManager.instance.coinPool[Coin.PLATINUM].Get();
        for (int i = 0; i < goldCount; i++) coins[index++] = PoolManager.instance.coinPool[Coin.GOLD].Get();
        for (int i = 0; i < silverCount; i++) coins[index++] = PoolManager.instance.coinPool[Coin.SILVER].Get();
        for (int i = 0; i < copperCount; i++) coins[index++] = PoolManager.instance.coinPool[Coin.COPPER].Get();

        return coins;
    }
}
