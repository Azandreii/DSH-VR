using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.GettingStarted;
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
            ButtonVR.OnClickStatic += ButtonVR_OnClickStatic;
            GameManager.Instance.OnDeselectTask += GameManager_OnDeselectTask;
        }

        if (selectedButton != null)
        {
            selectedButton.onClick.AddListener(() =>
            {
                GameManager.Instance.SetTaskSO(taskSO, gameObject);
                selectedButton.Select();
            });
        }
    }

    private void GameManager_OnDeselectTask(object sender, System.EventArgs e)
    {
        NotSelectedTask();
    }

    private void ButtonVR_OnClickStatic(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            UpdateSelectedButtonVisual(selectedButtonVR.isClicked());
        }
    }

    private void SelectedButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            GameManager.Instance.SetTaskSO(taskSO, gameObject);
            UpdateSelectedButtonVisual(selectedButtonVR.isClicked());
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
        }
        taskDifficultyText.text = _taskSO.taskDifficulty.ToString();
    }

    public void UpdateSelectedButtonVisual(bool _isSelected = false)
    {
        switch (_isSelected)
        {
            case true:
                SelectedTask();
                break;
            case false:
                if (GameManager.Instance.GetTaskSO() != taskSO)
                {
                    NotSelectedTask();
                }
                break;
        }
    }

    public void SelectedTask()
    {
        float _alpha = 0.3f;
        selectedButtonVR.SetImage(Color.black, _alpha);
    }

    public void NotSelectedTask()
    {
        float _alpha = 0.1f;
        selectedButtonVR.SetImage(Color.green, _alpha);
    }
}
