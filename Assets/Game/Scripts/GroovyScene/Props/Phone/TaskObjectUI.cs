using Sirenix.OdinInspector;
//using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskObjectUI : MonoBehaviour
{
    [Title("References")]
    [FoldoutGroup("Images"), InfoBox("The images of the TaskObjectUI are referenced here")]
    [SerializeField] private Image background;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the TaskObjectUI are referenced here. Select only one of the buttons to be referenced")]
    [SerializeField] private Button selectedButton;
    [FoldoutGroup("Buttons")]
    [SerializeField] private ButtonVR selectedButtonVR;
    //[SerializeField] private TaskManager taskManager;
    [FoldoutGroup("Texts"), InfoBox("The texts of the TaskObjectUI are referenced here")]
    [SerializeField] private TextMeshProUGUI taskNameText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI taskDescriptionText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI taskDifficultyText;
    private TaskSO taskSO;

    private void Start()
    {
        if (selectedButtonVR != null)
        {
            selectedButtonVR.OnClick += SelectedButtonVR_OnClick;
        }

        if (selectedButton != null)
        {
            selectedButton.onClick.AddListener(() =>
            {
                GameManager.Instance.SetTaskSO(taskSO, gameObject);
            });
        }
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (selectedButtonVR != null && e.taskSO == taskSO)
        {
            selectedButtonVR.OnClick -= SelectedButtonVR_OnClick;
        }
        if (selectedButton != null && e.taskSO == taskSO)
        {
            selectedButton.onClick.RemoveListener(() =>
            {
                GameManager.Instance.SetTaskSO(taskSO, gameObject);
            });
        }
    }

    private void SelectedButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            GameManager.Instance.SetTaskSO(taskSO, gameObject);
        }
    }

    public void SetTaskSO(TaskSO _taskSO)
    {
        taskSO = _taskSO;
        taskNameText.text = GetTaskName();
        taskDescriptionText.text = GetTaskDescription();
        SetTaskDifficulty(_taskSO);
        gameObject.SetActive(true);
    }

    public string GetTaskName()
    {
        return taskSO.taskName;
    }

    public string GetTaskDescription()
    {
        return taskSO.taskDescription;
    }

    public int GetTaskDifficulty()
    {
        return taskSO.taskDifficulty;
    }

    public void SetTaskDifficulty(TaskSO _taskSO)
    {
        switch (_taskSO.taskDifficulty)
        {
            case 1:
                taskDifficultyText.color = Color.green;
                break;
            case 2:
                taskDifficultyText.color = Color.yellow;
                break;
            case 3:
                taskDifficultyText.color = Color.red;
                break;
            case 4:
                taskDifficultyText.color = Color.blue;
                break;
            case 5:
                taskDifficultyText.color = Color.magenta;
                break;
        }
        taskDifficultyText.text = _taskSO.taskDifficulty.ToString();
    }
}
