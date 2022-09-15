using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : AbstractManager<CameraManager> {
    
    public GameObject cameraObject;
    public Camera mainCamera;


    public void ShakeCamera(float shakeDuration, float shakeStrength, int shakeVibrato) {
        mainCamera.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato);
    }

}
