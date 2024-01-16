using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternVisuals : MonoBehaviour, ITriggerCheckable
{
    #region TriggerCheckables Integration

    public bool isGivenWorkCheckable { get; set; }
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
        WaitingForTask,
        BecameBored,
        Working,
        WaitingForApproval,
        Unavailable,
    }

    [Header("References")]
    [SerializeField] private InternManager internManager;

    // State machine animations
    [SerializeField] private bool isAwaitingTaskState;
    [SerializeField] private bool isBored;
    [SerializeField] private bool isWorking;
    [SerializeField] private bool isWaitingForApproval;
    [SerializeField] private bool isUnavailable;

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
        internManager.OnStateChanged += InternManager_OnStateChanged;

        StateMachine.Initialize(AwaitingTaskState);

        //state frame update
        StateMachine.currentInternState.FrameUpdate();
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        switch (e.internState)                              // add bored as well
        {
            case InternManager.InternState.Idle:

                break;
            case InternManager.InternState.WorkingOnTask:

                break;
            case InternManager.InternState.WaitingForApproval:

                break;
            case InternManager.InternState.Unavailable:

                break;
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

    public void SetIsGivenWorkStatus(bool _isGivenWork)
    {
        isGivenWorkCheckable = _isGivenWork;
    }

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

    #endregion

    //MoveIntern


}
