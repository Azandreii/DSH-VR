using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForApproval : InternState
{
    public WaitingForApproval(InternVisuals internVisuals, InternStateMachine internStateMachine) : base(internVisuals, internStateMachine)
    {
    }

    public override void AnimationTriggerEvent(InternVisuals.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
