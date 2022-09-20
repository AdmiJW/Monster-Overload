using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputManager : AbstractManager<InputManager> {

    [Header("References")]
    public InputActionAsset controls;

    public PlayerControlStruct player;
    public MenuControlStruct menu;


    private List<InputActionMap> actionMaps;


    //============================
    // Lifecycle
    //============================
    protected override void Awake() {
        base.Awake();
        InitializeActionMaps();
    }



    //============================
    // Public
    //============================
    public void DisableAllActionMap() {
        foreach (InputActionMap actionMap in actionMaps) actionMap.Disable();
    }


    public void EnableOnlyActionMap(InputActionMap actionMap) {
        DisableAllActionMap();
        actionMap.Enable();
    }


    //============================
    // Helper
    //============================
    void InitializeActionMaps() {
        player.actionMap = controls.FindActionMap("PlayerControl");
        menu.actionMap = controls.FindActionMap("MenuControl");

        actionMaps = new List<InputActionMap>() {
            player.actionMap,
            menu.actionMap
        };
    }
}
