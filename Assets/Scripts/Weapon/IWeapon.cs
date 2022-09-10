using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Because attack animation may be slow,
// Starting the sequence (Toggle animation) and the time it takes to actually hit the enemy will not the same
// So, I make 2 methods:
//      1. TriggerAttack() - Start the attack sequence
//      2. DealDamage() - Deal damage to enemies
public interface IWeapon {
    public void Initialize(GameObject player, ContactFilter2D enemyContactFilter);
    public void TriggerAttack();
    public void DealDamage();
}
