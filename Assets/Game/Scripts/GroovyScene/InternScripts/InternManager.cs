using Newtonsoft.Json.Bson;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class InternManager : MonoBehaviour
{

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public InternState internState;
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

    public enum InternState
    {
        Idle,
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
    private float generalEfficiency = 1f;
    private float progress;
    [SerializeField] private InternState state;
    private float energyEfficiency = 1f;
    private float processEfficiency = 1f;
    private float availableEnergyEfficiency = 15f;
    private float workingEnergyEfficiency = -25f;
    private float awaitApprovalEnergyEfficiency = -3f;
    private float unavaiableEnergyEfficiency = 10f;
    private float techEfficiency = 1f;
    private float artEfficiency = 1f;
    private float designEfficiency = 1f;
    private float economyEfficiency = 1f;
    private float communicationEfficiency = 1f;
    private float teamworkEfficiency = 1f;
    private float defaultEffieciency = 1f;


    [Title("Set Intern Settings")]
    [SerializeField] bool setInternOnAwake = false;
    [SerializeField] InternSO setInternSO;
    private SpawnSpot currentSpot;

    #region AwaitingTask Variables

    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;

    #endregion



    private void Awake()
    {


        if (setInternOnAwake)
        {
            SetInternSO(setInternSO);
            if (internObjectUI.GetStopButtonObject() != null)
            {
                internObjectUI.GetStopButtonObject().gameObject.SetActive(false);
            }
            internObjectUI.GetInternTaskObject().gameObject.SetActive(false);
        }

    }




    private void Start()
    {

        if (setInternOnAwake)
        {
            if (InternSpawner.Instance != null)
            {
                InternSpawner.Instance.AddInternToActiveInternList(setInternSO);
                PhoneManager.Instance.SetInternSO(setInternSO);
            }
            if (InternSpawnerObject.Instance != null)
            {
                InternSpawnerObject.Instance.AddInternToActiveInternList(setInternSO, this);
                PhoneManager.Instance.SetInternSO(setInternSO);
            }
        }
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;


    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (taskSO == e.taskSO)
        {
            taskSO = null;
            state = InternState.Idle;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTaskCompleted -= GameManager_OnTaskCompleted;
    }

    private void Update()
    {

        switch (state)
        {
            case InternState.Idle:
                //State Idle

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { internState = state });
                if (currentEnergy <= energyMax)
                {
                    AdjustEnergy(availableEnergyEfficiency, energyEfficiency);
                    if (workingEnergyEfficiency != GetWorkingEfficiency())
                    {
                        SetWorkingEfficiency(GetWorkingEfficiency());
                    }

                    if (currentEnergy >= energyMax) { currentEnergy = energyMax; }
                }
                else { currentEnergy = energyMax; }
                break;

            case InternState.WorkingOnTask:
                //State WorkingOnTask

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { internState = state });
                progress -= Time.deltaTime * processEfficiency * CalculateTaskEfficiency();
                if (currentEnergy >= 0) { AdjustEnergy(workingEnergyEfficiency, energyEfficiency); }
                if (progress < 0)
                {
                    //Set State to WaitingForApproval
                    state = InternState.WaitingForApproval;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { internState = state });
                }
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = 1 - progress / progressMax,
                    eventTaskSO = taskSO,
                });
                break;

            case InternState.WaitingForApproval:
                //State Waiting for approval

                if (currentEnergy >= 0) { AdjustEnergy(awaitApprovalEnergyEfficiency, energyEfficiency); }
                break;

            case InternState.Unavailable:
                //State Unavailable

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { internState = state });
                if (currentEnergy <= energyMax) { AdjustEnergy(unavaiableEnergyEfficiency, energyEfficiency); }

                //Set State to Idle
                else { currentEnergy = energyMax; state = InternState.Idle; }
                break;
        }
        if (currentEnergy <= 0 && state != InternState.Unavailable)
        {
            //Set State to Unavailable
            SetInternState(InternState.Unavailable);
        }


    }

  

  

    public void SetInternSO(InternSO _internSO, bool _hide = true)
    {
        internSO = _internSO;
        SetEfficiency(GetEnergyEfficiency(), GetProcessEfficiency());
        SetEnergy(GetStartEnergy(), GetEnergyMax());
        SetGeneralEfficiency(GetGeneralEfficiency());
        SetStateEfficiency(GetAvailableEfficiency(), GetWorkingEfficiency(),
            GetAwaitApprovalEfficiency(), GetUnavailableEfficiency());
        SetSpecialtyEfficiency(GetTechEfficiency(), GetArtEfficiency(), GetDesignEfficiency(),
            GetEconomyEfficiency(), GetCommunicationEfficiency(), GetTeamworkEfficiency());
        internObjectUI.SetInternName();
        gameObject.SetActive(_hide);
    }

    public InternSO GetInternSO()
    {
        return internSO;
    }

    public void SetInternState(InternState _state)
    {
        this.state = _state;
        if (_state == InternState.Unavailable)
        {
            taskSO = null;
        }
    }

    public void SetInternManagerTask(TaskSO _taskSO, GameObject _gameObjectTaskSO)
    {
        if (_taskSO.isRechargeTask)
        {
            taskSO = _taskSO;
            DifficultySwitch(_taskSO);
            if (_gameObjectTaskSO != null)
            {
                gameObjectInternSO = _gameObjectTaskSO;
            }
            else
            {
                Debug.LogWarning("No GameObject attached to taskSO to destroy");
            }

            SetWorkingEfficiency(_taskSO.taskRechargeAmount);
            this.state = InternState.WorkingOnTask;
        }
        else
        {
            taskSO = _taskSO;
            DifficultySwitch(_taskSO);
            gameObjectInternSO = _gameObjectTaskSO;
            this.state = InternState.WorkingOnTask;
        }

        //Set State to WorkingOnTask
    }

    public InternState GetInternState()
    {
        return this.state;
    }

    public void PlayerApproved()
    {
        if (state == InternState.WaitingForApproval)
        {
            state = InternState.Idle;

            //Set State to Idle

            if (taskSO == SelectedSpawnerTaskSO.Instance.GetSelectedTaskSO())
            {
                taskSO = null;
            }
        }
    }

    public void AdjustEnergy(float _value, float _energyEfficiency, bool _withTimeDelta = true)
    {
        if (_withTimeDelta)
        {
            currentEnergy += _value * Time.deltaTime * _energyEfficiency * generalEfficiency;
        }
        else
        {
            currentEnergy += _value * _energyEfficiency;
        }
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs { energyNormalized = currentEnergy / energyMax });
    }

    public void SetGeneralEfficiency(float _generalEfficiency)
    {
        generalEfficiency = _generalEfficiency;
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

    public void SetWorkingEfficiency(float _working)
    {
        workingEnergyEfficiency = _working;
    }

    public void SetSpecialtyEfficiency(float _tech, float _art, float _design, float _economy, float _organisitation, float _research)
    {
        techEfficiency = _tech;
        artEfficiency = _art;
        designEfficiency = _design;
        economyEfficiency = _economy;
        communicationEfficiency = _organisitation;
        teamworkEfficiency = _research;
    }

    public void SetPoint(SpawnSpot _spawnSpot)
    {
        currentSpot = _spawnSpot;
        _spawnSpot.SetOccupied(true);
    }

    public void RemovePoint()
    {
        currentSpot.SetOccupied(false);
        currentSpot = null;
    }

    private float CalculateTaskEfficiency()
    {
        switch (taskSO.taskSpecialty)
        {
            case TaskSO.taskTheme.Tech:
                return techEfficiency;
            case TaskSO.taskTheme.Art:
                return artEfficiency;
            case TaskSO.taskTheme.Design:
                return designEfficiency;
            case TaskSO.taskTheme.Economy:
                return economyEfficiency;
            case TaskSO.taskTheme.Communication:
                return communicationEfficiency;
            case TaskSO.taskTheme.Teamwork:
                return teamworkEfficiency;
            case TaskSO.taskTheme.Default:
                return defaultEffieciency;
        }
        Debug.LogError("No task theme set!");
        return 0f;
    }

    private void DifficultySwitch(TaskSO _taskSO)
    {
        progressMax = _taskSO.GetTaskDifficulty();
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

    public float GetGeneralEfficiency()
    {
        return internSO.generalEfficiency;
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

    public float GetTechEfficiency()
    {
        if (internSO.selectedTech)
        {
            return internSO.techEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetArtEfficiency()
    {
        if (internSO.selectedArt)
        {
            return internSO.artEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetDesignEfficiency()
    {
        if (internSO.selectedDesign)
        {
            return internSO.designEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetEconomyEfficiency()
    {
        if (internSO.selectedEconomy)
        {
            return internSO.economyEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetCommunicationEfficiency()
    {
        if (internSO.selectedCommunication)
        {
            return internSO.communicationEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetTeamworkEfficiency()
    {
        if (internSO.selectedTeamwork)
        {
            return internSO.teamworkEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public SpawnSpot GetPoint()
    {
        return currentSpot;
    }

    public void ClearTaskSO()
    {
        taskSO = null;
    }

}
