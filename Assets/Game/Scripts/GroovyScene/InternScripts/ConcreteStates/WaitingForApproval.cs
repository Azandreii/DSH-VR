using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForApproval : InternState
{
    public WaitingForApproval(InternManager internManager, InternStateMachine internStateMachine) : base(internManager, internStateMachine)
    {
    }

    public override void AnimationTriggerEvent(InternManager.AnimationTriggerType triggerType)
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
