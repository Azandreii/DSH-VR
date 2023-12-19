using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>
{
    public virtual void Enter(T t)
    {
        Debug.Log($"Help 1" + GetType());
    }

    public virtual void Execute(T t)
    {

    }

    public virtual void Exit(T t)
    {
        Debug.Log($"Help 1" + GetType());
    }
}
