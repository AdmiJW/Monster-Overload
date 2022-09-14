using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatUIInitializer : AbstractSceneInitializer {
    
    public GameObject healthBarGroup;
    public TMP_Text coinText;

    void Awake() {
        PlayerManager.instance.healthBarGroup = healthBarGroup;
        PlayerManager.instance.coinText = coinText;
    }
}
