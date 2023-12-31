using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Diagnostics.CodeAnalysis;

public class InternObjectUI : MonoBehaviour
{
    [Title("References")]

    private bool buttonToggle;
    [FoldoutGroup("Buttons"),Button("Swap Button Type")]
    private void SwapButtons()
    {
        this.buttonToggle = !this.buttonToggle;
    }

    [SerializeField] private InternManager internManager;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the InternObjectUI are referenced here. Only select normal or VR buttons. Not both."), HideIf("buttonToggle")]
    [SerializeField] private Button assignButton;
    [FoldoutGroup("Buttons"), HideIf("buttonToggle")]
    [SerializeField] private Button approveButton;
    [FoldoutGroup("Buttons"), HideIf("buttonToggle")]
    [SerializeField] private Button stopButton;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the InternObjectUI are referenced here. Only select normal or VR buttons. Not both."), ShowIf("buttonToggle")]
    [SerializeField] private ButtonVR assignButtonVR;
    [FoldoutGroup("Buttons"), ShowIf("buttonToggle")]
    [SerializeField] private ButtonVR approveButtonVR;
    [FoldoutGroup("Buttons"), ShowIf("buttonToggle")]
    [SerializeField] private ButtonVR stopButtonVR;

    [FoldoutGroup("Buttons"), ShowIf("buttonToggle")]
    [SerializeField] private OnCollisionVR assignBodyTriggerVR;
    [FoldoutGroup("Buttons"), ShowIf("buttonToggle")]
    [SerializeField] private OnCollisionVR approveTriggerLeftHandVR;
    [FoldoutGroup("Buttons"), ShowIf("buttonToggle")]
    [SerializeField] private OnCollisionVR approveTriggerRightHandVR;

    [FoldoutGroup("Texts"), InfoBox("The texts of the InternObjectUI are referenced here")]
    [SerializeField] private TextMeshProUGUI assignText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI internName;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI internTask;
    [FoldoutGroup("Sliders"), InfoBox("The sliders of the InternObjectUI are referenced here")]
    [SerializeField] private Slider energyBar;
    [FoldoutGroup("Sliders")]
    [SerializeField] private Slider progressBar;
    [FoldoutGroup("GameObjects"), InfoBox("The gameObject references are referenced here")]
    [SerializeField] private GameObject progressBarObject;
    [FoldoutGroup("GameObjects")]
    [SerializeField] private GameObject internStopButtonObject;
    [FoldoutGroup("GameObjects")]
    [SerializeField] private GameObject internTaskObject;
    [FoldoutGroup("GameObjects")]
    [SerializeField] private GameObject stopItem;
    private bool taskAvailable = false;

    //[Header("Attributes")]

    [Title("Set Intern Settings")]
    [SerializeField] bool setInternOnAwake = false;
    [SerializeField] InternSO setInternSO;

    private void Awake()
    {
        if (setInternOnAwake)
        {
            internManager.SetInternSO(setInternSO);
            if (internStopButtonObject != null)
            {
                internStopButtonObject.gameObject.SetActive(false);
            }
            internTaskObject.gameObject.SetActive(false);
        }
    }

    private void Start()

    {
        if (setInternOnAwake)
        {
            int aditionalIntern = 1;
            InternSpawner.Instance.AdjustInternCount(aditionalIntern);
            InternSpawner.Instance.AddInternToActiveInternList(setInternSO);
        }
        taskAvailable = TaskManager.Instance.HasTasks();
        if (assignButton != null)
        {
            assignButton.onClick.AddListener(() =>
            {
                SetTask();
            });
        }

        if (approveButton != null)
        {
            approveButton.onClick.AddListener(() =>
            {
                ApproveTask();
            });
        }

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(() =>
            {
                internManager.SetInternState(InternManager.State.Available);
            });
        }

        TaskManager.Instance.OnTaskAdded += TaskManager_OnTaskAdded;
        internManager.OnStateChanged += InternManager_OnStateChanged;
        
        if (assignButtonVR != null)
        {
            assignButtonVR.OnClick += AssignButtonVR_OnClick;
        }

        if (approveButtonVR != null)
        {
            approveButtonVR.OnClick += ApproveButtonVR_OnClick;
        }

        if (stopButtonVR != null)
        {
            stopButtonVR.OnClick += StopButtonVR_OnClick;
        }

        if (approveTriggerLeftHandVR != null) 
        {
            approveTriggerLeftHandVR.OnCollisionControler += ApproveTriggerLeftHandVR_OnCollision;
        }

        if (approveTriggerRightHandVR != null)
        {
            approveTriggerRightHandVR.OnCollisionControler += ApproveTriggerRightHandVR_OnCollision;
        }

        if (assignBodyTriggerVR != null)
        {
            assignBodyTriggerVR.OnCollisionControler += AssignTriggerVR_OnCollision;
            assignBodyTriggerVR.OnCollisionGameObject += AssignBodyTriggerVR_OnCollisionGameObject;
        }
    }

    private void AssignBodyTriggerVR_OnCollisionGameObject(object sender, OnCollisionVR.SelectedObjectsEventArgs e)
    {
        if (e.collisionObject == stopItem && !e.collisionObject.GetComponent<StopItemVR>().IsStashed())
        {
            StopTask();
        }
    }

    private void TaskManager_OnTaskAdded(object sender, EventArgs e)
    {
        taskAvailable = TaskManager.Instance.HasTasks();
        if (!TaskManager.Instance.HasTasks() && internManager.GetInternState() != InternManager.State.Unavailable)
        {
            internManager.SetInternState(InternManager.State.Available);
        }
    }

    private void AssignTriggerVR_OnCollision(object sender, EventArgs e)
    {
        SetTask();
    }

    private void ApproveTriggerRightHandVR_OnCollision(object sender, EventArgs e)
    {
        ApproveTask();
    }

    private void ApproveTriggerLeftHandVR_OnCollision(object sender, EventArgs e)
    {
        ApproveTask();
    }

    private void StopButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            StopTask();
        }
    }

    private void ApproveButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            ApproveTask();
        }
    }

    private void AssignButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown && internManager.GetTaskSO() == null)
        {
            SetTask();
        }
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        switch (e.state)
        {
            case InternManager.State.Available:
                AssignTextColor(Color.white);
                SetTaskVisibilityUI(false);
                break;
            case InternManager.State.WorkingOnTask:
                AssignTextColor(Color.white);
                SetTaskVisibilityUI(true);
                break;
            case InternManager.State.WaitingForApproval:
                AssignTextColor(Color.white);
                SetTaskVisibilityUI(true);
                break;
            case InternManager.State.Unavailable:
                AssignTextColor(Color.red);
                SetTaskVisibilityUI(false);
                break;
        }
    }

    public void SetInternName()
    {
        internName.text = internManager.GetInternName();
    }

    private void SetTaskVisibilityUI(bool _visibility)
    {
        progressBarObject.SetActive(_visibility);
        if (internStopButtonObject != null)
        {
            internStopButtonObject.SetActive(_visibility);
        }
        internTaskObject.SetActive(_visibility);
    }

    private void SetTask()
    {
        if (internManager.GetInternState() == InternManager.State.Available && taskAvailable && GameManager.Instance.hasTask())
        {
            internManager.SetInternManagerTask(GameManager.Instance.GetTaskSO(), GameManager.Instance.GetGameObjectTaskSO());
            TaskSO _assignedTaskSO = GameManager.Instance.GetTaskSO();
            internTask.text = _assignedTaskSO.taskName;
            GameManager.Instance.SetTaskSO(null);
        }
    }

    private void ApproveTask()
    {
        if (internManager.GetInternState() == InternManager.State.WaitingForApproval)
        {
            //Could be an event
            GameManager.Instance.AddTaskCompleted(internManager.GetTaskSO(), internManager.GetGameObjectTaskSO());
            internManager.PlayerApproved();
        }
    }

    private void StopTask()
    {
        internManager.SetInternState(InternManager.State.Available);
        internManager.ClearTaskSO();
    }

    private void AssignTextColor(Color _color)
    {
        if (assignText != null)
        {
            assignText.color = _color;
        }
        
    }
}
