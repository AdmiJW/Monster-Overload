using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEmitter: MonoBehaviour {

    [Header("References")]
    public GameObject coinPrefab;

    [Header("Monster Drop")]
    public int minCoinDrop = 1;
    public int maxCoinDrop = 1;
    
    public float minInitialForce = 20f;
    public float maxInitialForce = 20f;
    
    


    public void Activate() {
        int dropAmount = Random.Range(minCoinDrop, maxCoinDrop);

        for (int i = 0; i < dropAmount; i++) {
            GameObject coin = GameObject.Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Rigidbody2D coinRigidbody = coin.GetComponent<Rigidbody2D>();

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float initialForce = Random.Range(minInitialForce, maxInitialForce);
            coinRigidbody.AddForce(randomDirection * initialForce, ForceMode2D.Impulse);
        }
    }
}
