using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



// Serves to initialize DialogueManager on scene load
public class DialogueUIInitializer : AbstractSceneInitializer {
    
    [Header("References")]
    public GameObject dialogBoxComponent;
    public TMP_Text dialogTextComponent;
    public TMP_Text actorNameTextComponent;

    [Space]
    public GameObject[] choicesComponent;


    void Awake() {
        DialogueManager.instance.actorNameTextComponent = actorNameTextComponent;
        DialogueManager.instance.dialogTextComponent = dialogTextComponent;
        DialogueManager.instance.dialogBoxComponent = dialogBoxComponent;
        DialogueManager.instance.choicesComponent = choicesComponent;
    }
}
