using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static InternObjectUI;
using UnityEngine.UIElements;

public class InternManager : MonoBehaviour
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public enum State
    {
        Available,
        WorkingOnTask,
        WaitingForApproval,
        Unavailable,
    }

    [Header("Attributes")]
    [SerializeField] private float progressMax = 3f;
    private float progress;
    [SerializeField] private State state;

    private void Update()
    {
        switch (state) {
            case State.Available:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            break;
            case State.WorkingOnTask:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                progress -= Time.deltaTime;
                if (progress < 0)
                {
                    state = State.WaitingForApproval;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                Debug.Log("Get Normalized Value = " + progress / progressMax);
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = 1 - progress / progressMax
                });
                break;
            case State.WaitingForApproval:
                Debug.Log("Waiting for Approval");
            break;
            case State.Unavailable:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            break;  
        }
    }

    public void SetInternState (State _state)
    {
        this.state = _state;
    }

    public void SetProgressTimerMax (float _value)
    {
        progressMax = _value;
        progress = _value;
    }

    public State GetInternState()
    {
        return this.state;
    }

    public void PlayerApproved()
    {
        if(state == State.WaitingForApproval)
        {
            state = State.Available;
        }
    }
}
