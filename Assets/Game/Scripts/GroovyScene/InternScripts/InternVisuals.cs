using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class InternVisuals : MonoBehaviour, ITriggerCheckable
{
    private const string WALKING = "Walking";
    private const string WORKING = "Working";
    private const string HIGHFIVEABLE = "HighFiveable";
    private const string UNAVAILABLE = "Unavailable";
    private const string HIGHFIVED = "HighFived"; 
    private const string BORED = "BecameBored";

    #region TriggerCheckables Integration

    public bool isAwaitingTaskCheckable { get; set; }
    public bool isBoredCheckable { get; set; }
    public bool isWaitingForApprovalCheckable { get; set; }
    public bool isWorkingCheckable { get; set; }
    public bool isUnavailableCheckable { get; set; }
    public bool isHighFivedCheckable { get; set; }

    #endregion

    #region State Machine Variable

    public InternStateMachine StateMachine { get; set; }
    public Working WorkingState { get; set; }
    public WaitingForApproval WaitingForApprovalState { get; set; }
    public Bored BoredState { get; set; }
    public AwaitingTask AwaitingTaskState { get; set; }
    public Unavailable UnavailableState { get; set; }
    public HighFived HighFivedState { get; set; }


    #endregion

    public enum AnimationTriggerType //types of triggers for anim
    {
        AwaitingTaskState,
        BecameBored,
        Working,
        WaitingForApproval,
        HighFived,
        Unavailable,
    }

    [Header("References")]
    [SerializeField] private InternManager internManager;
    [SerializeField] private GameObject workTablet;

    // State machine animations
    public bool isAwaitingTaskState;
    public bool isBored;
    public bool isWorking;
    public bool isWaitingForApproval;
    public bool isUnavailable;
    public bool isHighFived;
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
        HighFivedState = new HighFived(this, StateMachine);

        //setting work tablet visual to false
        workTablet.SetActive(false);
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
            //SetEveryBoolFalse();


            switch (e.internState)
            {
                case InternManager.InternState.Idle:

                    SetEveryBoolFalse();
                    timer = 0;
                    animationVisualState = AnimationTriggerType.AwaitingTaskState;
                    SetIsAwaitingTaskState(true);
                    animator.SetBool(WORKING, false);
                    Debug.Log("Set state to Idle");
                    break;

                case InternManager.InternState.WorkingOnTask:

                    animationVisualState = AnimationTriggerType.Working;
                    SetIsWorkingStatus(true);
                    animator.SetBool(WORKING, true);
                    Debug.Log("Set state to Working");
                    workTablet.SetActive(true);
                    break;

                case InternManager.InternState.WaitingForApproval:

                    animationVisualState = AnimationTriggerType.WaitingForApproval;
                    SetIsWaitingForApprovalStatus(true);
                    animator.SetBool(HIGHFIVEABLE, true);
                    Debug.Log("Set state to WaitingForApproval");
                    workTablet.SetActive(false);
                    break;

                case InternManager.InternState.Unavailable:

                    animationVisualState = AnimationTriggerType.Unavailable;
                    SetIsUnavailable(true);
                    animator.SetBool(UNAVAILABLE, true);
                    Debug.Log("Set state to Unavailable");
                    break;

                case InternManager.InternState.HighFived:
                    animationVisualState = AnimationTriggerType.HighFived;
                    SetIsHighFived(true);
                    animator.SetBool(HIGHFIVED, true);
                    Debug.Log("Set state to HighFived");
                    break;
                /* case InternManager.InternState.HighFived:
                    animationVisualState = AnimationTriggerType.HighFived;
                    SetIsHighFived(true);
                    anim.SetBool("HighFived", true);
                        Debug.Log("Set state to HighFived");
                    break; */

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
                animator.SetBool(BORED, true);
                Debug.Log("Set state to Bored");
            }

        /* if (internManager.GetInternState() == InternManager.InternState.Unavailable && InternManager.current )
         {
             anim.ResetTrigger(
         } */

        if (timer <= 30.0f && animationVisualState == AnimationTriggerType.AwaitingTaskState && GameStateManager.Instance.IsGamePlaying())
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        StateMachine.currentInternState.PhysicsUpdate();
    }

    //Not being used Andrei
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

    public void SetIsHighFived (bool _isHighFivedCheckable)
    {
        isHighFived = _isHighFivedCheckable;
    }


    public void SetEveryStateFalse()
    {
        SetIsWaitingForApprovalStatus(false);
        SetIsUnavailable(false);
        SetIsWorkingStatus(false);
        SetIsBoredStatus(false);
        SetIsAwaitingTaskState(false);
        SetIsHighFived(false);
    }

    public void SetEveryBoolFalse()
    {
        Debug.Log("Reseting all animator bools to false");
        animator.SetBool(BORED, false);
        animator.SetBool(HIGHFIVEABLE, false);
        animator.SetBool(HIGHFIVED, false);
        animator.SetBool(WORKING, false);
        animator.SetBool(WALKING, false);
        animator.SetBool(UNAVAILABLE, false);
    }

    public void PlayHighfive()
    {
        animator.SetBool(HIGHFIVED, true);
    }
    //MoveIntern
}
