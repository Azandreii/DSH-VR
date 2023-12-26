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
        public TaskSO eventTaskSO;
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

    [Header("References")]
    [SerializeField] private InternObjectUI internObjectUI;
    private InternSO internSO;
    private GameObject gameObjectInternSO;
    private TaskSO taskSO;

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

    private void Start()
    {
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (taskSO == e.taskSO)
        {
            taskSO = null;
            state = State.Available;
        }
    }

    private void Update()
    {
        switch (state) {
            case State.Available:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax) { AdjustEnergy(availableEnergyEfficiency, energyEfficiency);
                    if (currentEnergy >= energyMax) { currentEnergy = energyMax; }
                }
                else { currentEnergy = energyMax; }
                break;
            case State.WorkingOnTask:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                progress -= Time.deltaTime * processEfficiency;
                if (currentEnergy >= 0) { AdjustEnergy(workingEnergyEfficiency, energyEfficiency); }
                if (progress < 0)
                {
                    state = State.WaitingForApproval;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
                    progressNormalized = 1 - progress / progressMax,
                    eventTaskSO = taskSO,
                });
                break;
            case State.WaitingForApproval:
                if (currentEnergy >= 0) { AdjustEnergy(awaitApprovalEnergyEfficiency, energyEfficiency); }
                break;
            case State.Unavailable:
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                if (currentEnergy <= energyMax) { AdjustEnergy(unavaiableEnergyEfficiency, energyEfficiency); }
                else { currentEnergy = energyMax; state = State.Available; }
                break;
        }
        if (currentEnergy <= 0 && state != State.Unavailable)
        {
            SetInternState(State.Unavailable);
        }
    }

    public void SetInternSO(InternSO _internSO, bool _hide = true)
    {
        internSO = _internSO;
        SetEfficiency(GetEnergyEfficiency(), GetProcessEfficiency());
        SetEnergy(GetStartEnergy(), GetEnergyMax());
        SetStateEfficiency(GetAvailableEfficiency(), GetWorkingEfficiency(), 
            GetAwaitApprovalEfficiency(), GetUnavailableEfficiency());
        internObjectUI.SetInternName();
        gameObject.SetActive(_hide);
    }

    public InternSO GetInternSO()
    {
        return internSO;
    }

    public void SetInternState(State _state)
    {
        this.state = _state;
        if(_state == State.Unavailable)
        {
            taskSO = null;
        }
    }

    public void SetTask(TaskSO _taskSO, GameObject _gameObjectTaskSO)
    {
        taskSO = _taskSO;
        DifficultySwitch(_taskSO.taskDifficulty);
        gameObjectInternSO = _gameObjectTaskSO;
        this.state = State.WorkingOnTask;
    }

    public State GetInternState()
    {
        return this.state;
    }

    public void PlayerApproved()
    {
        if (state == State.WaitingForApproval)
        {
            state = State.Available;
            taskSO = null;
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

    public void SetEnergy(float _currentEnergyValue, float _maxEnergyValue)
    {
        energyMax = _maxEnergyValue;
        currentEnergy = _currentEnergyValue;
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void SetEfficiency(float _energyEfficiency, float _processEfficiency)
    {
        energyEfficiency = _energyEfficiency;
        processEfficiency = _processEfficiency;
    }

    public void SetStateEfficiency(float _available, float _working, float _awaitingApproval, float _unavailable)
    {
        availableEnergyEfficiency = _available;
        workingEnergyEfficiency = _working;
        awaitApprovalEnergyEfficiency = _awaitingApproval;
        unavaiableEnergyEfficiency = _unavailable;
    }

    private void DifficultySwitch(float _difficultyGrade)
    {
        switch (_difficultyGrade)
        {
            case 1:
                float easyTaskTime = 3f;
                progressMax = easyTaskTime;
                break;
            case 2:
                float mediumTaskTime = 7f;
                progressMax = mediumTaskTime;
                break;
            case 3:
                float hardTaskTime = 11f;
                progressMax = hardTaskTime;
                break;
        }
        progress = progressMax;
    }

    public TaskSO GetTaskSO()
    {
        return taskSO;
    }
    
    public GameObject GetGameObjectTaskSO()
    {
        return gameObjectInternSO;
    }

    public Transform GetInternObjectUI()
    {
        return internSO.internObjectUI;
    }

    public string GetInternName()
    {
        return internSO.internName;
    }

    public float GetStartEnergy()
    {
        return internSO.startEnergy;
    }

    public float GetEnergyMax()
    {
        return internSO.maxEnergy;
    }

    public float GetProcessEfficiency()
    {
        return internSO.processEfficiency;
    }

    public float GetEnergyEfficiency()
    {
        return internSO.energyEfficiency;
    }

    public float GetAvailableEfficiency()
    {
        return internSO.availableEfficiency;
    }

    public float GetWorkingEfficiency()
    {
        return internSO.workingEfficiency;
    }

    public float GetAwaitApprovalEfficiency()
    {
        return internSO.awaitApprovalEfficiency;
    }

    public float GetUnavailableEfficiency()
    {
        return internSO.unavailableEfficiency;
    }

    public void ClearTaskSO()
    {
        taskSO = null;
    }
}
