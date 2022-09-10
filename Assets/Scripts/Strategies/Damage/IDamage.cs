using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage {
    void DealDamage(GameObject target);
    void SetActive(bool active);
}
