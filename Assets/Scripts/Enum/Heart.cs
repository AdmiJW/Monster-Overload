
public enum Heart {
    RED,
    GREEN,
    GOLDEN,
}




static class HeartExtMethods {
    public static int GetHealValue(this Heart heart) {
        if (heart == Heart.RED) return 3;
        if (heart == Heart.GREEN) return 10;
        return 50;
    }
}