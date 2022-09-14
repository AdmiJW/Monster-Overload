using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


// Teleports the player to specificed scene when the player enters the trigger area.
public class SceneTeleporter : MonoBehaviour {

    [Header("Scene")]
    public int sceneIndex;
    public Vector2 spawnPosition;
    public Direction spawnFaceDirection = Direction.DOWN;

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") return;

        // Save player's data to PlayerManager
        PlayerPersistanceScript persistance = other.gameObject.GetComponent<PlayerPersistanceScript>();
        persistance.SaveToPlayerManager();
        PlayerManager.instance.spawnPosition = spawnPosition;
        PlayerManager.instance.spawnFaceDirection = spawnFaceDirection;

        StartCoroutine(LoadSceneAsync(sceneIndex));
    }


    IEnumerator LoadSceneAsync(int sceneIndex) {
        LoadingScreen.instance.UpdateLoadingPercentage(0);
        LoadingScreen.instance.ShowLoadingScreen();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        op.completed += OnSceneLoadComplete;
        
        while (!op.isDone) {
            LoadingScreen.instance.UpdateLoadingPercentage(op.progress * 100);
            yield return null;
        }
    }


    void OnSceneLoadComplete(AsyncOperation op) {
        LoadingScreen.instance.HideLoadingScreen();
        Time.timeScale = 1f;
    }
}
