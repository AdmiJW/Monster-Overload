using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Coin {
    COPPER,
    SILVER,
    GOLD,
    PLATINUM
}




static class CoinExtMethods {
    public static int GetCoinValue(this Coin dir) {
        if (dir == Coin.COPPER) return 1;
        if (dir == Coin.SILVER) return 10;
        if (dir == Coin.GOLD) return 50;
        return 100;
    }
}