using System.Collections.Generic;


public enum WeaponType {
    PUNCH,
    SWORD,
    BOW,
    FIRE_STAFF,
}



// extension methods
public static class WeaponTypeExtension {
    public static Dictionary<WeaponType, string> weaponTypeToName = new Dictionary<WeaponType, string>() {
        {WeaponType.PUNCH, "Punch"},
        {WeaponType.SWORD, "Sword"},
        {WeaponType.BOW, "Bow"},
        {WeaponType.FIRE_STAFF, "Fire Staff"},
    };


    public static string Name(this WeaponType weaponType) {
        return weaponTypeToName[weaponType];
    }
}