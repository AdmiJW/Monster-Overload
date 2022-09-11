using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    int value = 1;

    void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        ItemAudioManager.instance.coin.Play();
        collision.gameObject.GetComponent<PlayerCoinScript>().AddCoin(value);
        Destroy(gameObject);
    }
}
