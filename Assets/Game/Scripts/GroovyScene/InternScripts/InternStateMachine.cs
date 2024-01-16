using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternStateMachine 
{
    public InternState currentInternState { get; set; }

    public void Initialize(InternState startingState)
    {
        currentInternState = startingState;
        currentInternState.EnterState();
    }

    public void ChangeState(InternState newState)
    {
        currentInternState.ExitState();
        currentInternState = newState;
        currentInternState.EnterState();
    }

}
