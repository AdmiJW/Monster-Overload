using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class DialogueManager : MonoBehaviour {
    // Singleton
    public static DialogueManager instance { get; private set; }


    [Header("Configuration")]
    public float textSpeed = 0.05f;


    [Header("Dialog Box")]
    public GameObject dialogBoxComponent;
    public TMP_Text dialogTextComponent;
    public TMP_Text actorNameTextComponent;

    [Header("Choices")]
    public GameObject[] choices;

    [Header("Controls")]
    public InputActionAsset controls;
    public InputActionReference selectButton;
    private InputActionMap dialogueActionMap;
    private InputActionMap playerActionMap;

    // States
    private bool isDisplayed = false;

    private string currentText;
    private IEnumerator currentTextCoroutine;
    private Action currentTextCouroutineCompletedCallback;

    //=====================================================
    // Lifecycle
    //=====================================================
    void Awake() {
        instance = this;

        dialogueActionMap = controls.FindActionMap("MenuControl");
        playerActionMap = controls.FindActionMap("PlayerControl");

        // Register event handler
        selectButton.action.performed += OnContinueButtonPressed;

        // TODO: Remove
        Invoke("test", 3f);
    }

    // TODO: Remove
    void test() {
        DisplayText("Hello, world! My name is Soh and I am proud to present you");
    }




    //=============================
    // Input handlers
    //=============================
    void OnContinueButtonPressed(InputAction.CallbackContext ctx) {
        // In middle of displaying text === Skip text
        if (currentTextCoroutine != null) {
            StopCoroutine(currentTextCoroutine);
            currentTextCoroutine = null;
            dialogTextComponent.text = currentText;
        }
    }

    
    void DisplayText(string text, Action completedCallback = null) {
        // If dialogbox is not yet open, open it.
        if (!isDisplayed) EnterDialogSession();

        currentText = text;
        currentTextCoroutine = DialogCoroutine(completedCallback);
        StartCoroutine(currentTextCoroutine);
    }



    // Coroutine to display text character by character
    IEnumerator DialogCoroutine(Action completionCallback = null) {
        dialogTextComponent.text = "";

        foreach (char c in currentText) {
            UIAudioManager.instance.dialogBlip.Play();
            dialogTextComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

        if (completionCallback != null) completionCallback();
    }




    void EnterDialogSession() {
        Time.timeScale = 0;
        isDisplayed = true;
        // Dialog box Animation
        dialogBoxComponent.GetComponent<Animator>().SetTrigger("Open");
        // Change input mode
        dialogueActionMap.Enable();
        playerActionMap.Disable();
    }

    void ExitDialogSession() {
        Time.timeScale = 1;
        isDisplayed = false;
        // Dialog box Animation
        dialogBoxComponent.GetComponent<Animator>().SetTrigger("Close");
        // Change input mode
        dialogueActionMap.Disable();
        playerActionMap.Enable();
    }
}
