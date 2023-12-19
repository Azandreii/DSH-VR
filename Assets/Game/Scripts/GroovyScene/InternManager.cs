using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public event EventHandler<OnEnergyChangedEventArgs> OnEnergyChanged;
    public class OnEnergyChangedEventArgs : EventArgs
    {
        public float energyNormalized;
    }

    public enum State
    {
        Available,
        WorkingOnTask,
        WaitingForApproval,
        Unavailable,
    }

    [Header("Attributes")]
    [SerializeField] private float energyMax = 300f;
    [SerializeField] private float currentEnergy = 200f;
    [SerializeField] private float progressMax = 3f;
    private float progress;
    [SerializeField] private State state;
    private float energyEfficiency = 1f;
    private float processEfficiency = 1f;
    private float availableEnergyEfficiency = 15f;
    private float workingEnergyEfficiency = -25f;
    private float awaitApprovalEnergyEfficiency = -3f;
    private float unavaiableEnergyEfficiency = 10f;

    private void Update()
    {
        switch (state) {
            case State.Available:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax){ AdjustEnergy(availableEnergyEfficiency, energyEfficiency); }
            break;
            case State.WorkingOnTask:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                progress -= Time.deltaTime * processEfficiency;
                if (currentEnergy >= 0){ AdjustEnergy(workingEnergyEfficiency, energyEfficiency); }
                else { state = State.Unavailable; }
                if (progress < 0)
                {
                    state = State.WaitingForApproval;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{ progressNormalized = 1 - progress / progressMax });
            break;
            case State.WaitingForApproval:
                if (currentEnergy >= 0){ AdjustEnergy(awaitApprovalEnergyEfficiency, energyEfficiency); }
                else { state = State.Unavailable; }
            break;
            case State.Unavailable:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax) { AdjustEnergy(unavaiableEnergyEfficiency, energyEfficiency); }
                else { state = State.Available; }
            break;  
        }

        Debug.Log(state);
    }

    public void SetTask(State _state, float _value)
    {
        this.state = _state;
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

    public void AdjustEnergy(float _value, float _energyEfficiency, bool _withTimeDelta = true)
    {
        if (_withTimeDelta)
        {
            currentEnergy += _value * Time.deltaTime * _energyEfficiency;
        }
        else
        {
            currentEnergy += _value * _energyEfficiency;
        }
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void SetEnergy(float _value)
    {
        currentEnergy = _value;
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void SetEfficiency(float _energyEfficiency, float _processEfficiency)
    {
        energyEfficiency = _energyEfficiency;
        processEfficiency = _processEfficiency;
    }

    public void SetStateEfficiency(float _available, float _working, float _awaitingApproval, float _unavailable)
    {
        _available = availableEnergyEfficiency;
        _working = workingEnergyEfficiency;
        _awaitingApproval = awaitApprovalEnergyEfficiency;
        _unavailable = unavaiableEnergyEfficiency;
    }
}
