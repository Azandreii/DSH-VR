using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitingTask : InternState
{

    private Vector3 _targetPos;
    private Vector3 _direction;

    public AwaitingTask(InternManager internManager, InternStateMachine internStateMachine) : base(internManager, internStateMachine)
    {
    }

    public override void AnimationTriggerEvent(InternManager.AnimationTriggerType triggerType)
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

        _direction = (_targetPos - internManager.transform.position).normalized;

        //have to mage MoveIntern Function
       // internManager.MoveIntern(_direction * internManager.randomMovementSpeed);

        if ((internManager.transform.position - _targetPos).sqrMagnitude < 0.01f)
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
        return internManager.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * internManager.randomMovementRange;
    }

}
