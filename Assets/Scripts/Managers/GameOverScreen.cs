
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class GameOverScreen : AbstractManager<GameOverScreen> {
    
    [Header("References")]
    public CanvasGroup canvasGroup;
    public TMP_Text respawnPrompt;

    [Header("Config")]
    public float fadeDuration = 0.5f;

    private InputAction respawnAction;
    private bool isGameOver = false;


    //=========================================================================
    //  Lifecycle
    //=========================================================================
    protected override void Awake() {
        base.Awake();
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start() {
        this.respawnAction = InputManager.instance.menu.respawn.action;
    }


    public async void GameOver() {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0;
        MusicManager.instance.PlayGameOver();
        MusicManager.instance.FadeIn(fadeDuration);

        // Disable player input
        InputManager.instance.EnableOnlyActionMap( InputManager.instance.menu.actionMap );

        await ShowGameOverScreen();

        // Respawn button
        respawnAction.performed += RespawnAtCurrentScene;
    }



    async Task ShowGameOverScreen() {
        respawnPrompt.text = "";
        
        canvasGroup.DOFade(1, fadeDuration).SetUpdate(true);
        await Task.Delay( (int)(fadeDuration * 1000) );
        
        respawnPrompt.text = "Press [" + respawnAction.controls[0].displayName.ToUpper() + "] to respawn";
    }


    async void RespawnAtCurrentScene(InputAction.CallbackContext ctx) {
        isGameOver = false;
        respawnAction.performed -= RespawnAtCurrentScene;
        UIAudioManager.instance.menuSelect.Play();

        await GameManager.instance.ReloadCurrentSceneAsync();

        MusicManager.instance.Stop();
        InputManager.instance.EnableOnlyActionMap( InputManager.instance.player.actionMap );
        canvasGroup.alpha = 0;
    }

}
