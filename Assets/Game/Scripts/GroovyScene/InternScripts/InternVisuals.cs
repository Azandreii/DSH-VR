using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
    //public InternManager.InternState initialState;
    //public InternManager.InternState finalState;
    public Animator animator;
    public float timer = 0f;
    [SerializeField] private AnimationTriggerType animationVisualState;

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
            SetEveryBoolFalse();


            switch (e.internState)
            {
                case InternManager.InternState.Idle:

                    timer = 0;
                    animationVisualState = AnimationTriggerType.AwaitingTaskState;
                    SetIsAwaitingTaskState(true);
                    Debug.Log("Set state to Idle");
                    break;

                case InternManager.InternState.WorkingOnTask:

                    animationVisualState = AnimationTriggerType.Working;
                    SetIsWorkingStatus(true);
                    animator.SetBool("Working", true);
                    Debug.Log("Set state to Working");
                    break;

                case InternManager.InternState.WaitingForApproval:

                    animationVisualState = AnimationTriggerType.WaitingForApproval;
                    SetIsWaitingForApprovalStatus(true);
                    animator.SetBool("HighFiveable", true);
                    Debug.Log("Set state to WaitingForApproval");
                    break;

                case InternManager.InternState.Unavailable:

                    animationVisualState = AnimationTriggerType.Unavailable;
                    SetIsUnavailable(true);
                    animator.SetBool("Working", true);
                    Debug.Log("Set state to Unavailable");
                    break;
            }

            

        }
    }

    private void Update()
    {
            if (timer >= 30f && internManager.GetInternState() == InternManager.InternState.Idle) // and check if bored parameter is still false  
            {
                timer = 0f;
                SetEveryStateFalse();
                animationVisualState = AnimationTriggerType.BecameBored;
                SetIsBoredStatus(true);
                SetEveryBoolFalse();
                animator.SetBool("BecameBored", true);
                Debug.Log("Set state to Bored");
            }

        if (internManager.GetInternState() == InternManager.InternState.Unavailable && InternManager.currente )
        {
            animator.ResetTrigger(
        }


        if (timer <= 30.0f && animationVisualState == AnimationTriggerType.AwaitingTaskState && GameStateManager.Instance.IsGamePlaying())
            {
                timer += Time.deltaTime;
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

    public void SetEveryBoolFalse()
    {
        animator.SetBool("GivenTask", false);
        animator.SetBool("WaitingForTask", false);
        animator.SetBool("BecameBored", false);
        animator.SetBool("HighFiveable", false);
        animator.SetBool("HighFived", false);
        animator.SetBool("Working", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Unavailable", false);
    }

    //MoveIntern


}
