using System.Collections.Generic;


public enum WeaponType {
    PUNCH,
    SWORD,
    BOW,
    FIRE_STAFF,
    ENEMY,
}



// extension methods
public static class WeaponTypeExtMethods {
    private static Dictionary<WeaponType, string> weaponTypeToName = new Dictionary<WeaponType, string>() {
        {WeaponType.PUNCH, "Punch"},
        {WeaponType.SWORD, "Sword"},
        {WeaponType.BOW, "Bow"},
        {WeaponType.FIRE_STAFF, "Fire Staff"},
        {WeaponType.ENEMY, "Enemie's Weapon"},
    };


    public static string Name(this WeaponType weaponType) {
        return weaponTypeToName[weaponType];
    }
}