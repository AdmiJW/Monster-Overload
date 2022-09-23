using System;

public class NullRangeTrigger : AbstractRangeTrigger {
    public override void RegisterEnterEvent(Action action) {}
    public override void RegisterExitEvent(Action action) {}
    public override void UnregisterEnterEvent(Action action) {}
    public override void UnregisterExitEvent(Action action) {}
}
