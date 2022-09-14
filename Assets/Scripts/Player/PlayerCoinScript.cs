
using UnityEngine;
using TMPro;

public class PlayerCoinScript : MonoBehaviour {

    private int _balance;
    public int balance { 
        get { return _balance; } 
        set {
            _balance = value;
            UpdateText();
        }
    }

    private TMP_Text _text;


    void Awake() {
        if (PlayerManager.instance.coinText == null) return;
        
        this._text = PlayerManager.instance.coinText;
        this.balance = PlayerManager.instance.playerCoins;
    }


    public bool IsSufficient(int amount) {
        return balance >= amount;
    }

    
    public void UpdateText() {
        PlayerManager.instance.coinText.text = balance.ToString();
        PlayerManager.instance.coinText.color = ( balance < 0? Color.red: Color.black );
    }
}
