using UnityEngine;

public class EntityPrefab : MonoBehaviour {
    // Singleton
    public static EntityPrefab instance;
    void Awake() { instance = this; }


    // Prefabs
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public GameObject slimePrefab;
}
