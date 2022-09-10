using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PrefabManager : MonoBehaviour {
    [Header("Entities")]
        public GameObject player;
        public GameObject slime;

    [Header("Items")]
        public GameObject coin;


    // Access
    public static EntityPrefabsStruct entityPrefabs;
    public static ItemPrefabsStruct itemPrefabs;




    void Awake() {
        entityPrefabs = new EntityPrefabsStruct {
            player = player,
            slime = slime
        };


        itemPrefabs = new ItemPrefabsStruct {
            coin = coin
        };
    }
}
