using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternState 
{

    protected InternVisuals internVisuals;
    protected InternStateMachine internStateMachine;

    public InternState ( InternVisuals internVisuals, InternStateMachine internStateMachine)
    {
        this.internVisuals = internVisuals;
        this.internStateMachine = internStateMachine;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void FrameUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(InternVisuals.AnimationTriggerType triggerType) { }

}
