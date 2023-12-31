using Newtonsoft.Json.Bson;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
    private float organisationEfficiency = 1f;
    private float researchEfficiency = 1f;
    private float defaultEffieciency = 1f;

    [Title("Set Intern Settings")]
    [SerializeField] bool setInternOnAwake = false;
    [SerializeField] InternSO setInternSO;

    [Title("Task Level Effiency")]
    [SerializeField] private float easyDifficulty = 3f;
    [SerializeField] private float mediumDifficulty = 7f;
    [SerializeField] private float hardDifficulty = 11f;

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
            int _aditionalIntern = 1;
            if (InternSpawner.Instance != null)
            {
                InternSpawner.Instance.AdjustInternCount(_aditionalIntern);
                InternSpawner.Instance.AddInternToActiveInternList(setInternSO);
                PhoneManager.Instance.SetInternSO(setInternSO);
            }
            if (InternSpawnerObject.Instance != null)
            {
                InternSpawnerObject.Instance.AdjustInternCount(_aditionalIntern);
                InternSpawnerObject.Instance.AddInternToActiveInternList(setInternSO);
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
            state = InternState.Available;
        }
    }

    private void Update()
    {
        switch (state) {
            case InternState.Available:
                //State Available

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { internState = state });
                if (currentEnergy <= energyMax) { AdjustEnergy(availableEnergyEfficiency, energyEfficiency);
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
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
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

                //Set State to Available
                else { currentEnergy = energyMax; state = InternState.Available; }
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
        SetStateEfficiency(GetAvailableEfficiency(), GetWorkingEfficiency(), 
            GetAwaitApprovalEfficiency(), GetUnavailableEfficiency());
        SetSpecialtyEfficiency(GetTechEfficiency(), GetArtEfficiency(), GetDesignEfficiency(),
            GetEconomyEfficiency(), GetOrganisationEfficiency(), GetResearchEfficiency());
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
        if(_state == InternState.Unavailable)
        {
            taskSO = null;
        }
    }

    public void SetInternManagerTask(TaskSO _taskSO, GameObject _gameObjectTaskSO)
    {
        taskSO = _taskSO;
        DifficultySwitch(_taskSO);
        gameObjectInternSO = _gameObjectTaskSO;
        this.state = InternState.WorkingOnTask;

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
            state = InternState.Available;

            //Set State to Available

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

    public void SetSpecialtyEfficiency(float _tech, float _art, float _design, float _economy, float _organisitation, float _research)
    {
        techEfficiency = _tech;
        artEfficiency = _art;
        designEfficiency = _design;
        economyEfficiency = _economy;
        organisationEfficiency = _organisitation;
        researchEfficiency = _research;
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
            case TaskSO.taskTheme.Organisation:
                return organisationEfficiency;
            case TaskSO.taskTheme.Research:
                return researchEfficiency;
            case TaskSO.taskTheme.Default:
                return defaultEffieciency;
        }
        Debug.Log("No task theme set!");
        return 0f;
    }

    private void DifficultySwitch(TaskSO _taskSO)
    {

        int _difficultyGrade = _taskSO.taskDifficulty;
        switch (_difficultyGrade)
        {
            case 1:
                progressMax = easyDifficulty;
                break;
            case 2:
                progressMax = mediumDifficulty;
                break;
            case 3:
                progressMax = hardDifficulty;
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

    public float GetOrganisationEfficiency()
    {
        if (internSO.selectedOrganisation)
        {
            return internSO.organisationEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public float GetResearchEfficiency()
    {
        if (internSO.selectedResearch)
        {
            return internSO.researchEfficiency;
        }
        float baseModifier = 1f;
        return baseModifier;
    }

    public void ClearTaskSO()
    {
        taskSO = null;
    }
}
