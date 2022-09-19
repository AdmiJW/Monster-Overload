
using UnityEngine;

public class DropEmitter: MonoBehaviour {

    [Header("Monster Drop")]
    public int minCoinDrop = 1;
    public int maxCoinDrop = 1;

    [Range(0f, 1f)]
    public float heartDropChance = 0.083f;
    public int minHeartDrop = 1;
    public int maxHeartDrop = 1;

    
    public float minInitialForce = 20f;
    public float maxInitialForce = 20f;
    
    


    public void Activate() {
        GameObject[] coins = GetCoinDrops();
        GameObject[] hearts = GetHeartDrops();

        foreach (GameObject coin in coins) ShootPickup(coin);
        foreach (GameObject heart in hearts) ShootPickup(heart);
    }


    GameObject[] GetCoinDrops() {
        int dropAmount = Random.Range(minCoinDrop, maxCoinDrop+1);

        int platinumCount = dropAmount / Coin.PLATINUM.GetCoinValue();
        dropAmount -= platinumCount * Coin.PLATINUM.GetCoinValue();
        int goldCount = dropAmount / Coin.GOLD.GetCoinValue();
        dropAmount -= goldCount * Coin.GOLD.GetCoinValue();
        int silverCount = dropAmount / Coin.SILVER.GetCoinValue();
        dropAmount -= silverCount * Coin.SILVER.GetCoinValue();
        int copperCount = dropAmount / Coin.COPPER.GetCoinValue();
        dropAmount -= copperCount * Coin.COPPER.GetCoinValue();

        GameObject[] coins = new GameObject[platinumCount + goldCount + silverCount + copperCount];
        int index = 0;
        for (int i = 0; i < platinumCount; i++) coins[index++] = CoinPool.instance.coinPool[Coin.PLATINUM].Get();
        for (int i = 0; i < goldCount; i++) coins[index++] = CoinPool.instance.coinPool[Coin.GOLD].Get();
        for (int i = 0; i < silverCount; i++) coins[index++] = CoinPool.instance.coinPool[Coin.SILVER].Get();
        for (int i = 0; i < copperCount; i++) coins[index++] = CoinPool.instance.coinPool[Coin.COPPER].Get();

        return coins;
    }


    GameObject[] GetHeartDrops() {
        if (Random.value > heartDropChance) return new GameObject[0];
        int dropAmount = Random.Range(minHeartDrop, maxHeartDrop+1);

        int goldenCount = dropAmount / Heart.GOLDEN.GetHealValue();
        dropAmount -= goldenCount * Heart.GOLDEN.GetHealValue();
        int greenCount = dropAmount / Heart.GREEN.GetHealValue();
        dropAmount -= greenCount * Heart.GREEN.GetHealValue();
        int redCount = dropAmount / Heart.RED.GetHealValue();

        GameObject[] hearts = new GameObject[goldenCount + greenCount + redCount];
        int index = 0;
        for (int i = 0; i < goldenCount; i++) hearts[index++] = HeartPool.instance.heartPool[Heart.GOLDEN].Get();
        for (int i = 0; i < greenCount; i++) hearts[index++] = HeartPool.instance.heartPool[Heart.GREEN].Get();
        for (int i = 0; i < redCount; i++) hearts[index++] = HeartPool.instance.heartPool[Heart.RED].Get();

        return hearts;
    }


    void ShootPickup(GameObject pickup) {
        Rigidbody2D rb = pickup.GetComponent<Rigidbody2D>();
        rb.MovePosition(transform.position);

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float initialForce = Random.Range(minInitialForce, maxInitialForce);
        rb.AddForce(randomDirection * initialForce, ForceMode2D.Impulse);
    }
}
