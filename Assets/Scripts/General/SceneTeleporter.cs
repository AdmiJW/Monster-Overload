
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


// Teleports the player to specificed scene when the player enters the trigger area.
public class SceneTeleporter : MonoBehaviour {

    [Header("Scene")]
    public int sceneIndex;
    public Vector2 spawnPosition;
    public Direction spawnFaceDirection = Direction.DOWN;

    
    void OnTriggerEnter2D(Collider2D other) {
        if ( !other.gameObject.CompareTag("Player") ) return;

        // Save player's data to PlayerManager
        PlayerPersistanceScript persistance = other.gameObject.GetComponent<PlayerPersistanceScript>();
        persistance.SaveToPlayerManager();
        PlayerManager.instance.spawnPosition = spawnPosition;
        PlayerManager.instance.spawnFaceDirection = spawnFaceDirection;

        GameManager.instance.LoadScene(sceneIndex);
    }
}
