using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    [Header("References")]
    public GameObject enemyPrefab;

    [Header("Spawner")]
    public float spawnCheckInterval = 15f;
    public int maximumSpawns = 5;
    public float radius = 5f;

    
    private GameObject[] spawnedEnemies;


    //===============================
    // Lifecycle
    //===============================
    void Awake() {
        spawnedEnemies = new GameObject[maximumSpawns];
    }

    void OnEnable() {
        InvokeRepeating("SpawnEnemy", 0f, spawnCheckInterval);
    }

    void OnDisable() {
        CancelInvoke("SpawnEnemy");
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



    //===============================
    // Spawning
    //===============================

    void SpawnEnemy() {
        // Spawn 1 enemy if there is vacant
        for (int i = 0; i < spawnedEnemies.Length; i++) {
            if (spawnedEnemies[i] != null) continue;
            
            float randomDistance = Random.Range(0f, radius);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            spawnedEnemies[i] = Instantiate(enemyPrefab, transform.position + (Vector3)(randomDirection * randomDistance), Quaternion.identity);

            return;
        }
    }
}
