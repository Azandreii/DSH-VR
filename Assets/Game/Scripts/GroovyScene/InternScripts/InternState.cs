using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternState 
{

    protected InternManager internManager;
    protected InternStateMachine internStateMachine;

    public InternState ( InternManager internManager, InternStateMachine internStateMachine)
    {
        this.internManager = internManager;
        this.internStateMachine = internStateMachine;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void FrameUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(InternManager.AnimationTriggerType triggerType) { }

}
