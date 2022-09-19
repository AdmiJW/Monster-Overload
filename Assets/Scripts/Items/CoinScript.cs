using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    
    [Header("Coin")]
    public Coin coinType;


    void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        ItemAudioManager.instance.coin.Play();
        collision.gameObject.GetComponent<PlayerCoinScript>().balance += coinType.GetCoinValue();
        
        gameObject.SetActive(false);
        PoolManager.instance.coinPool[coinType].Release(gameObject);
    }
}
