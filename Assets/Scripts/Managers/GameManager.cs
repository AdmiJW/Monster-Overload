using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


// Everything under GameManager will not be destroyed between scenes
public class GameManager : AbstractManager<GameManager> {

    public LayerMask PLAYER_LAYER_MASK;
    public LayerMask ENEMY_LAYER_MASK;
    public LayerMask MAP_LAYER_MASK;
    public ContactFilter2D ENEMY_CONTACT_FILTER;

    
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        InitializeLayerMaskAndFilters();
    }


    //==================================
    // Scene Loading
    //==================================
    public async Task LoadSceneAsync(int sceneIndex) {
        Time.timeScale = 0f;
        LoadingScreen.instance.UpdateLoadingPercentage(0);
        await LoadingScreen.instance.ShowLoadingScreenAsync();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        PoolManager.instance.ReleaseAll();
        
        while (!op.isDone) {
            LoadingScreen.instance.UpdateLoadingPercentage( op.progress * 100 );
            await Task.Delay(100);
        }

        // Task shall complete without waiting for loading screen to hide
        LoadingScreen.instance.HideLoadingScreen();
        Time.timeScale = 1f;
    }


    public async Task ReloadCurrentSceneAsync() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        await LoadSceneAsync(currentSceneIndex);
    }

    public async void LoadScene(int sceneIndex) {
        await LoadSceneAsync(sceneIndex);
    }

    public async void ReloadCurrentScene() {
        await ReloadCurrentSceneAsync();
    }


    //=====================================
    // Helpers
    //=====================================
    void InitializeLayerMaskAndFilters() {
        PLAYER_LAYER_MASK = LayerMask.GetMask("Player");
        ENEMY_LAYER_MASK = LayerMask.GetMask("Enemies");
        MAP_LAYER_MASK = LayerMask.GetMask("Map");

        ENEMY_CONTACT_FILTER = new ContactFilter2D();
        ENEMY_CONTACT_FILTER.SetLayerMask(ENEMY_LAYER_MASK);
    }

}
