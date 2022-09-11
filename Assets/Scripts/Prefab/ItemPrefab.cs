using UnityEngine;

public class ItemPrefab : MonoBehaviour {
    // Singleton
    public static ItemPrefab instance;
    void Awake() { instance = this; }


    // Prefabs
    [Header("Collectibles")]
    public GameObject coinPrefab;
}

