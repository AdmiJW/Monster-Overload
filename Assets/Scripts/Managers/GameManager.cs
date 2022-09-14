
using UnityEngine;


// Everything under GameManager will not be destroyed between scenes
public class GameManager : AbstractManager<GameManager> {

    public ContactFilter2D ENEMY_CONTACT_FILTER;

    
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        ENEMY_CONTACT_FILTER = new ContactFilter2D();
        ENEMY_CONTACT_FILTER.SetLayerMask(LayerMask.GetMask("Enemies"));
    }

}
