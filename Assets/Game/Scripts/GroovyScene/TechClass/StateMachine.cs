using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generics
public class StateMachine<T>
{
    T owner;
    State<T> currentState;
    State<T> previousState;

    public StateMachine(T _owner)
    {
        this.owner = _owner;
    }

    public void Execute()
    {
        if(currentState == null)
        {
            return;
        }

        currentState.Execute(owner);
    }

    public void ChangeState(State<T> newState)
    {
        //Do not execute if newState is incorrect
        if (newState == null)
        {
            return;
        }

        //Exit previous state
        currentState.Exit(owner);

        //Set old state
        previousState = currentState;

        //Change the variable to new state
        currentState = newState;

        //Enter the new state
        currentState.Enter(owner);
    }

    public void RevertToPreviousState()
    {
        if (previousState != null) { }
    }
}
