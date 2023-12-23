using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskObjectUI : MonoBehaviour
{
    [Title("References")]
    [FoldoutGroup("Images"), InfoBox("The images of the TaskObjectUI are referenced here")]
    [SerializeField] private Image background;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the TaskObjectUI are referenced here")]
    [SerializeField] private Button selectedButton;
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
        selectedButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetTaskSO(taskSO, gameObject);
            selectedButton.Select();
        });
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
}
