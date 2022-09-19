using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LoadingScreen : AbstractManager<LoadingScreen> {

    public TMP_Text loadingText;
    public Animator animator;


    public async Task ShowLoadingScreen() {
        animator.SetTrigger("Show");
        await Task.Delay(500);
    }

    public async Task HideLoadingScreen() {
        animator.SetTrigger("Hide");
        await Task.Delay(500);
    }

    public void UpdateLoadingPercentage(float percentage) {
        loadingText.text = "Loading " + percentage + "%";
    }

}
