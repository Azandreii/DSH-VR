using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InternVisuals : MonoBehaviour, ITriggerCheckable
{
    #region TriggerCheckables Integration

    public bool isAwaitingTaskCheckable { get; set; }
    public bool isBoredCheckable { get; set; }
    public bool isWaitingForApprovalCheckable { get; set; }
    public bool isWorkingCheckable { get; set; }
    public bool isUnavailableCheckable { get; set; }

    #endregion

    #region State Machine Variable

    public InternStateMachine StateMachine { get; set; }
    public Working WorkingState { get; set; }
    public WaitingForApproval WaitingForApprovalState { get; set; }
    public Bored BoredState { get; set; }
    public AwaitingTask AwaitingTaskState { get; set; }
    public Unavailable UnavailableState { get; set; }


    #endregion

    public enum AnimationTriggerType //types of triggers for anim
    {
        AwaitingTaskState,
        BecameBored,
        Working,
        WaitingForApproval,
        Unavailable,
    }

    [Header("References")]
    [SerializeField] private InternManager internManager;

    // State machine animations
    public bool isAwaitingTaskState;
    public bool isBored;
    public bool isWorking;
    public bool isWaitingForApproval;
    public bool isUnavailable;
    public InternManager.InternState lastState = InternManager.InternState.Idle;
    public InternManager.InternState nextState;

    #region AwaitingTask Variables

    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        //seting up states
        StateMachine = new InternStateMachine();
        WorkingState = new Working(this, StateMachine);
        WaitingForApprovalState = new WaitingForApproval(this, StateMachine);
        BoredState = new Bored(this, StateMachine);
        AwaitingTaskState = new AwaitingTask(this, StateMachine);
        UnavailableState = new Unavailable(this, StateMachine);
    }

    private void Start()
    {
        lastState = internManager.GetInternState();

        internManager.OnStateChanged += InternManager_OnStateChanged;

        StateMachine.Initialize(AwaitingTaskState);

        //state frame update
        StateMachine.currentInternState.FrameUpdate();
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        // add bored as well

        lastState = nextState; //save current state
        nextState = e.internState;
        Debug.Log(e.internState + " " + nextState);

        if (lastState != nextState)
        {
            SetEveryStateFalse();


            switch (e.internState)
            {
                case InternManager.InternState.Idle:

                    SetIsAwaitingTaskState(true);
                    Debug.Log("Set state to Idle");
                    break;

                case InternManager.InternState.WorkingOnTask:

                    SetIsWorkingStatus(true);
                    Debug.Log("Set state to Working");
                    break;

                case InternManager.InternState.WaitingForApproval:

                    SetIsWaitingForApprovalStatus(true);
                    Debug.Log("Set state to WaitingForApproval");
                    break;

                case InternManager.InternState.Unavailable:

                    SetIsUnavailable(true);
                    Debug.Log("Set state to Unavailable");
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.currentInternState.PhysicsUpdate();
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.currentInternState.AnimationTriggerEvent(triggerType);
    }

    #region Distance Checks

    public void SetIsBoredStatus(bool _isBored)
    {
        isBoredCheckable = _isBored;
    }

    public void SetIsWaitingForApprovalStatus(bool _isWaitingForApprovalCheckable)
    {
        isWaitingForApprovalCheckable = _isWaitingForApprovalCheckable;
    }

    public void SetIsWorkingStatus(bool _isWorking)
    {
        isWorkingCheckable = _isWorking;
    }

    public void SetIsUnavailable(bool _isUnavailable)
    {
        isUnavailableCheckable = _isUnavailable;
    }

    public void SetIsAwaitingTaskState(bool _isAwaitingTaskCheckable)
    {
        isAwaitingTaskCheckable = _isAwaitingTaskCheckable;
    } 

    public void SetEveryStateFalse()
    {
        SetIsWaitingForApprovalStatus(false);
        SetIsUnavailable(false);
        SetIsWorkingStatus(false);
        SetIsBoredStatus(false);
        SetIsAwaitingTaskState(false);
    }

    

    #endregion

    //MoveIntern


}
