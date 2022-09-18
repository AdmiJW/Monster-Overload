using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatUIInitializer : AbstractSceneInitializer {
    
    public GameObject healthBarGroup;
    public TMP_Text coinText;
    public TMP_Text statusText;

    void Awake() {
        PlayerManager.instance.healthBarGroup = healthBarGroup;
        PlayerManager.instance.coinText = coinText;
        PlayerManager.instance.statusText = statusText;
    }
}
