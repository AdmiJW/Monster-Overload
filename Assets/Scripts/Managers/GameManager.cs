
using UnityEngine;


// Everything under GameManager will not be destroyed between scenes
public class GameManager : AbstractManager<GameManager> {

    public LayerMask PLAYER_LAYER_MASK;
    public LayerMask ENEMY_LAYER_MASK;
    public LayerMask MAP_LAYER_MASK;
    public ContactFilter2D ENEMY_CONTACT_FILTER;

    
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        PLAYER_LAYER_MASK = LayerMask.GetMask("Player");
        ENEMY_LAYER_MASK = LayerMask.GetMask("Enemies");
        MAP_LAYER_MASK = LayerMask.GetMask("Map");

        ENEMY_CONTACT_FILTER = new ContactFilter2D();
        ENEMY_CONTACT_FILTER.SetLayerMask(ENEMY_LAYER_MASK);
    }

}
