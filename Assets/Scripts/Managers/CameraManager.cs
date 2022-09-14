using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : AbstractManager<CameraManager> {
    
    public GameObject cameraObject;
    public Camera mainCamera;

    [Header("Camera Shaking Configuration")]
    public float shakeDuration = 0.3f;
    public float shakeStrength = 0.2f;
    public int shakeVibrato = 10;



    public void ShakeCamera() {
        mainCamera.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato);
    }

}
