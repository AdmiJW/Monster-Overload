
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

        LoadSceneAsync(sceneIndex);
    }


    // Activates loading screen and start the loading process
    async void LoadSceneAsync(int sceneIndex) {
        LoadingScreen.instance.UpdateLoadingPercentage(0);
        await LoadingScreen.instance.ShowLoadingScreen();
        Time.timeScale = 0f;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!op.isDone) {
            LoadingScreen.instance.UpdateLoadingPercentage(op.progress * 100);
            await Task.Delay(100);
        }

        await LoadingScreen.instance.HideLoadingScreen();
        Time.timeScale = 1f;
    }
}
