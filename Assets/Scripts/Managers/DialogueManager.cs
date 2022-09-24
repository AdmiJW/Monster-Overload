/**
 * DialogueManager.cs
 * @author AdmiJW 
 *
 * This is a Singleton Dialogue system for Unity game. Relies heavily on Ink narrative scripting language:
 * (https://www.inklestudios.com/ink/)
 *
 * This dialogue system mainly uses ink's tags (Prefix #) to register commands to the system, eg: Setting
 * the name of the actor.
 *
 * The controls are binded through InputManager singleton instance, which contains the action maps and actions
 *
 * Tag commands:
 * -    # ACTOR: <name>
 * -    
 * Example:
 *      # ACTOR: Professor Oak
 *      Please choose your pokemon
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Ink.Runtime;


public class DialogueManager : AbstractManager<DialogueManager> {


    // Configuration
    [Header("Configuration")]
    public float textSpeed = 0.05f;
    
    // Events
    public event Action onDialogEnd;


    [NonSerialized]
    public GameObject dialogBoxComponent;
    [NonSerialized]
    public TMP_Text dialogTextComponent;
    [NonSerialized]
    public TMP_Text actorNameTextComponent;
    [NonSerialized]
    public GameObject[] choicesComponent;

    //======================
    // States
    //======================
    private Story currentStory;
    private List<Choice> currentChoices;
    private int currentChoiceIndex = -1;

    private string currentText;
    private IEnumerator currentTextCoroutine;
    private Action currentTextCouroutineCompletedCallback;

    //=====================================================
    // Lifecycle
    //=====================================================
    void Start() {
        // Register event handler
        InputManager.instance.menu.select.action.performed += OnSelectButtonPressed;
        InputManager.instance.menu.up.action.performed += OnUpButtonPressed;
        InputManager.instance.menu.down.action.performed += OnDownButtonPressed;
    }


    //===============================
    // Public API
    //===============================
    public void StartStory(Story story) {
        currentStory = story;
        EnterDialogSession();
        Next();
    }


    //=============================
    // Input handlers
    //=============================
    void OnSelectButtonPressed(InputAction.CallbackContext ctx) {
        if (currentStory == null) return;
        Next();
    }

    
    void OnUpButtonPressed(InputAction.CallbackContext ctx) {
        if (currentStory == null || currentChoices == null) return;

        UIAudioManager.instance.menuMove.Play();
        currentChoiceIndex = Mathf.Max(0, currentChoiceIndex - 1);
        SetChoiceHighlight();
    }


    void OnDownButtonPressed(InputAction.CallbackContext ctx) {
        if (currentStory == null || currentChoices == null) return;

        UIAudioManager.instance.menuMove.Play();
        currentChoiceIndex = Mathf.Min(currentChoices.Count - 1, currentChoiceIndex + 1);
        SetChoiceHighlight();
    }

    
    //=============================
    // Logic
    //=============================
    void EnterDialogSession() {
        Time.timeScale = 0;
        // Dialog box Animation
        dialogBoxComponent.GetComponent<Animator>().SetTrigger("Open");
        // Change input mode
        InputManager.instance.EnableOnlyActionMap( InputManager.instance.menu.actionMap );
    }


    void ExitDialogSession() {
        Time.timeScale = 1;
        currentStory = null;
        // Dialog box Animation
        dialogBoxComponent.GetComponent<Animator>().SetTrigger("Close");
        // Notify listeners
        onDialogEnd?.Invoke();
        // Clear listeners.
        onDialogEnd = null;
        // Change input mode
        InputManager.instance.EnableOnlyActionMap( InputManager.instance.player.actionMap );
    }


    void Next() {
        if (currentTextCoroutine != null) SkipTextTyping();
        else if (currentChoices != null) SelectChoice();
        else if (currentStory.canContinue || currentStory.currentChoices.Count > 0) ContinueStory();
        else ExitDialogSession();
    }

    
    void ContinueStory() {
        currentText = currentStory.Continue();
        foreach(string cmd in currentStory.currentTags) ParseCommand(cmd);

        currentTextCouroutineCompletedCallback = null;

        if (currentStory.currentChoices.Count > 0)
            currentTextCouroutineCompletedCallback = () => PresentChoices(currentStory.currentChoices);

        currentTextCoroutine = DialogCoroutine(currentTextCouroutineCompletedCallback);
        StartCoroutine(currentTextCoroutine);
    }


    void SkipTextTyping() {
        StopCoroutine(currentTextCoroutine);
        dialogTextComponent.text = currentText;
        if (currentTextCouroutineCompletedCallback != null) currentTextCouroutineCompletedCallback();

        currentTextCoroutine = null;
        currentTextCouroutineCompletedCallback = null;
    }


    void ParseCommand(string cmd) {
        string[] args = cmd.Split(':');

        // Map commands
        if (cmd.StartsWith("ACTOR")) SetActor(args[1]);
        else Debug.LogError("Invalid command received from story: " + cmd);
    }


    //=============================
    // Choice Logic
    //=============================
    void PresentChoices(List<Choice> choices) {
        if (choices.Count > choicesComponent.Length) throw new Exception("Too many choices, not enough buttons");

        currentChoices = choices;
        currentChoiceIndex = 0;

        for (int i = 0; i < choices.Count; i++) {
            choicesComponent[i].SetActive(true);
            choicesComponent[i].GetComponentInChildren<TMP_Text>().text = choices[i].text;
        }
        
        SetChoiceHighlight();
    }


    void SelectChoice() {
        UIAudioManager.instance.menuSelect.Play();
        currentStory.ChooseChoiceIndex(currentChoiceIndex);
        currentChoices = null;

        foreach (GameObject choice in choicesComponent) choice.SetActive(false);
        Next();
    }



    //==============================
    // Command Executor
    //==============================
    void SetActor(string actorName) {
        actorNameTextComponent.text = actorName.Trim();
    }


    //==============================
    // UI
    //==============================
    void SetChoiceHighlight() {
        for (int i = 0; i < choicesComponent.Length; ++i) {
            if (!choicesComponent[i].activeSelf) continue;

            TMP_Text buttonText = choicesComponent[i].GetComponentInChildren<TMP_Text>();
            buttonText.color = i == currentChoiceIndex ? Color.red : Color.black;
            buttonText.fontStyle = i == currentChoiceIndex ? FontStyles.Bold : FontStyles.Normal;
        }
    }



  
    //=============================
    // Coroutines
    //=============================
    // Coroutine to display text character by character, like typing
    IEnumerator DialogCoroutine(Action completionCallback = null) {
        dialogTextComponent.text = "";

        foreach (char c in currentText) {
            UIAudioManager.instance.dialogBlip.Play();
            dialogTextComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

        if (completionCallback != null) completionCallback();
        currentTextCoroutine = null;
    }
}
