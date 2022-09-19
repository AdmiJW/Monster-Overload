
using UnityEngine;


public class HeartScript : MonoBehaviour {
    
    [Header("Heart")]
    public Heart heartType;


    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("PlayerCollector")) return;

        PlayerAudioManager.instance.heal.Play();
        GameObject player = collision.gameObject.transform.parent.gameObject;
        player.GetComponent<PlayerHealthScript>().Heal( heartType.GetHealValue() );

        PoolManager.instance.heartPool[heartType].Release(gameObject);
    }
}
