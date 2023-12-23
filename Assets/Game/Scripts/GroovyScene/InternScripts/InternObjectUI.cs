using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InternObjectUI : MonoBehaviour
{

    [Title("References")]
    [SerializeField] private InternManager internManager;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the InternObjectUI are referenced here")]
    [SerializeField] private Button assignButton;
    [FoldoutGroup("Texts"), InfoBox("The texts of the InternObjectUI are referenced here")]
    [SerializeField] private TextMeshProUGUI assignText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI internName;
    [FoldoutGroup("Buttons")]
    [SerializeField] private Button approveButton;
    [FoldoutGroup("Sliders"), InfoBox("The sliders of the InternObjectUI are referenced here")]
    [SerializeField] private Slider energyBar;
    [FoldoutGroup("Sliders")]
    [SerializeField] private Slider progressBar;
    [FoldoutGroup("GameObjects"), InfoBox("The gameObject references are referenced here")]
    [SerializeField] private GameObject progressBarObject;
    private bool taskAvailable = false;

    //[Header("Attributes")]

    private void Start()
    {
        taskAvailable = TaskManager.Instance.HasTasks();
        assignButton.onClick.AddListener(() =>
        {
            if (internManager.GetInternState() == InternManager.State.Available && taskAvailable)
            {
                if (GameManager.Instance.hasTask())
                {
                    internManager.SetTask(GameManager.Instance.GetTaskSO(), GameManager.Instance.GetGameObjectTaskSO());
                    GameManager.Instance.SetTaskSO(null);
                }
            }
        });
        approveButton.onClick.AddListener(() =>
        {
            if (internManager.GetInternState() == InternManager.State.WaitingForApproval)
            {
                GameManager.Instance.AddTaskCompleted(internManager.GetTaskSO(), internManager.GetGameObjectTaskSO());
                internManager.PlayerApproved();
            }
        });

        TaskManager.Instance.OnTaskAdded += TaskManager_OnTaskAdded;
        internManager.OnStateChanged += InternManager_OnStateChanged;
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
                progressBarObject.SetActive(false);
                break;
            case InternManager.State.WorkingOnTask:
                assignText.color = Color.white;
                progressBarObject.SetActive(true);
                break;
            case InternManager.State.WaitingForApproval:
                assignText.color = Color.white;
                progressBarObject.SetActive(true);
                break;
            case InternManager.State.Unavailable:
                assignText.color = Color.red;
                progressBarObject.SetActive(false);
                break;
        }
    }

    public void SetInternName()
    {
        internName.text = internManager.GetInternName();
    }
}
