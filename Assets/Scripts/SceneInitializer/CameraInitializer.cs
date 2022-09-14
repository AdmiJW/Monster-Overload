using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitializer : MonoBehaviour {
    
    public Camera mainCamera;

    void Awake() {
        CameraManager.instance.cameraObject = gameObject;
        CameraManager.instance.mainCamera = mainCamera;
    }

}
