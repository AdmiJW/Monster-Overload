using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropEmitter {

    // Used to get the position of the monster and access the Instantiate function
    private Transform origin;
    private int minDropAmount;
    private int maxDropAmount;
    private float minInitialForce;
    private float maxInitialForce;

    public MonsterDropEmitter(
        Transform origin,
        int minDropAmount,
        int maxDropAmount,
        float minInitialForce = 20f,
        float maxInitialForce = 40f
    ) {
        this.origin = origin;
        this.minDropAmount = minDropAmount;
        this.maxDropAmount = maxDropAmount;
        this.minInitialForce = minInitialForce;
        this.maxInitialForce = maxInitialForce;
    }


    public void Activate() {
        GameObject coinPrefab = PrefabManager.itemPrefabs.coin;
        int dropAmount = Random.Range(minDropAmount, maxDropAmount);

        for (int i = 0; i < dropAmount; i++) {
            GameObject coin = GameObject.Instantiate(coinPrefab, origin.position, Quaternion.identity);
            Rigidbody2D coinRigidbody = coin.GetComponent<Rigidbody2D>();

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float initialForce = Random.Range(minInitialForce, maxInitialForce);
            coinRigidbody.AddForce(randomDirection * initialForce, ForceMode2D.Impulse);
        }
    }

}
