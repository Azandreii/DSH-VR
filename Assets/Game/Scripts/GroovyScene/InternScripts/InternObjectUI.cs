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
    private bool taskAvailable = false;

    //[Header("Attributes")]

    private void Start()
    {
        taskAvailable = TaskManager.Instance.HasTasks();
        if (assignButton != null)
        {
            assignButton.onClick.AddListener(() =>
            {
                if (internManager.GetInternState() == InternManager.State.Available && taskAvailable && GameManager.Instance.hasTask())
                {
                    SetTask();
                }
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
        
        if (assignButtonVR !=  null)
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
    }

    private void StopButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            internManager.SetInternState(InternManager.State.Available);
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
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            SetTask();
        }
    }

    private void TaskManager_OnTaskAdded(object sender, TaskManager.OnTaskAddedEventArgs e)
    {
        taskAvailable = e.hasTasks;
        if (!e.hasTasks && internManager.GetInternState() != InternManager.State.Unavailable)
        {
            internManager.SetInternState(InternManager.State.Available);
        }
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        switch (e.state)
        {
            case InternManager.State.Available:
                assignText.color = Color.white;
                SetTaskVisibilityUI(false);
                break;
            case InternManager.State.WorkingOnTask:
                assignText.color = Color.white;
                SetTaskVisibilityUI(true);
                break;
            case InternManager.State.WaitingForApproval:
                assignText.color = Color.white;
                SetTaskVisibilityUI(true);
                break;
            case InternManager.State.Unavailable:
                assignText.color = Color.red;
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
        internStopButtonObject.SetActive(_visibility);
        internTaskObject.SetActive(_visibility);
    }

    private void SetTask()
    {
        internManager.SetTask(GameManager.Instance.GetTaskSO(), GameManager.Instance.GetGameObjectTaskSO());
        TaskSO _assignedTaskSO = GameManager.Instance.GetTaskSO();
        internTask.text = _assignedTaskSO.taskName;
        GameManager.Instance.SetTaskSO(null);
    }

    private void ApproveTask()
    {
        if (internManager.GetInternState() == InternManager.State.WaitingForApproval)
        {
            GameManager.Instance.AddTaskCompleted(internManager.GetTaskSO(), internManager.GetGameObjectTaskSO());
            internManager.PlayerApproved();
        }
    }
}
