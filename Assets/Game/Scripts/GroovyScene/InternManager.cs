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
    [SerializeField] private float currentEnergy;
    [SerializeField] private float progressMax = 3f;
    private float progress;
    [SerializeField] private State state;

    private void Update()
    {
        switch (state) {
            case State.Available:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax){ AddEnergy(15 * Time.deltaTime); }
            break;
            case State.WorkingOnTask:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                progress -= Time.deltaTime;
                if (currentEnergy >= 0){ RemoveEnergy(25 * Time.deltaTime); }
                else { state = State.Unavailable; }
                if (progress < 0)
                {
                    state = State.WaitingForApproval;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{ progressNormalized = 1 - progress / progressMax });
            break;
            case State.WaitingForApproval:
                if (currentEnergy >= 0){ RemoveEnergy(3 * Time.deltaTime); }
                else { state = State.Unavailable; }
            break;
            case State.Unavailable:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax) { AddEnergy(10 * Time.deltaTime); }
                else { state = State.Available; }
            break;  
        }
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

    public void AddEnergy(float _value)
    {
        currentEnergy += _value;
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void RemoveEnergy(float _value)
    {
        currentEnergy -= _value;
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void SetEnergy(float _value)
    {
        currentEnergy = _value;
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }
}
