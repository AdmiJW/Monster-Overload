using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// No invulnerability at all
public class NullInvulnerable : IInvulnerable {
    public void ActivateVulnerable() {}
    public bool IsInvulnerable() { return false; }
}
