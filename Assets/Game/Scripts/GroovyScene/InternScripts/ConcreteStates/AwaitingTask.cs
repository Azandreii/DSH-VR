using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitingTask : InternState
{

    private Vector3 _targetPos;
    private Vector3 _direction;

    public AwaitingTask(InternVisuals internVisuals, InternStateMachine internStateMachine) : base(internVisuals, internStateMachine)
    {
    }

    public override void AnimationTriggerEvent(InternVisuals.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - internVisuals.transform.position).normalized;

        //have to mage MoveIntern Function
       // internVisuals.MoveIntern(_direction * internVisuals.randomMovementSpeed);

        if ((internVisuals.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInCircle()
    {
        return internVisuals.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * internVisuals.randomMovementRange;
    }

}
