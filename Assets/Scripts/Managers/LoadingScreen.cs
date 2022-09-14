using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreen : AbstractManager<LoadingScreen> {

    public TMP_Text loadingText;
    public Animator animator;


    public void ShowLoadingScreen() {
        animator.SetTrigger("Show");
    }

    public void HideLoadingScreen() {
        animator.SetTrigger("Hide");
    }

    public void UpdateLoadingPercentage(float percentage) {
        loadingText.text = "Loading " + percentage + "%";
    }

}
