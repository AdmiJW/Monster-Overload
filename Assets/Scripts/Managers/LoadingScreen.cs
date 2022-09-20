
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LoadingScreen : AbstractManager<LoadingScreen> {

    public TMP_Text loadingText;
    public Animator animator;


    public async void ShowLoadingScreen() {
        await ShowLoadingScreenAsync();
    }

    public async void HideLoadingScreen() {
        await HideLoadingScreenAsync();
    }

    public async Task ShowLoadingScreenAsync() {
        animator.SetTrigger("Show");
        await Task.Delay(500);
    }

    public async Task HideLoadingScreenAsync() {
        animator.SetTrigger("Hide");
        await Task.Delay(500);
    }

    public void UpdateLoadingPercentage(float percentage) {
        loadingText.text = "Loading " + percentage.ToString("0.##") + "%";
    }

}
