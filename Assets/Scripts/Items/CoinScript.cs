
using UnityEngine;


public class CoinScript : MonoBehaviour {
    
    [Header("Coin")]
    public Coin coinType;


    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("PlayerCollector")) return;

        ItemAudioManager.instance.coin.Play();
        GameObject player = collision.gameObject.transform.parent.gameObject;
        player.GetComponent<PlayerCoinScript>().balance += coinType.GetCoinValue();
        
        PoolManager.instance.coinPool[coinType].Release(gameObject);
    }
}
