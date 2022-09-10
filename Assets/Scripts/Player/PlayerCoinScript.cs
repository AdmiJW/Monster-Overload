using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoinScript : MonoBehaviour {
    
    public int Balance { get; private set; }
    private TMP_Text balanceText;


    void Awake() {
        balanceText = GameObject
            .FindWithTag("InGameUI")
            .transform
            .Find("UICoin")
            .transform
            .Find("CoinText")
            .GetComponent<TMP_Text>();
    }


    public void AddCoin(int amount) {
        Balance += amount;
        balanceText.text = Balance.ToString();
    }

    public void DeductCoin(int amount) {
        Balance = Mathf.Max(0, Balance - amount);
        balanceText.text = Balance.ToString();
    }

    public void UpdateBalance(int amount) {
        Balance = amount;
        balanceText.text = Balance.ToString();
    }

}
