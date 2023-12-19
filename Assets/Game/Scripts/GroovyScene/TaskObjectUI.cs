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
    //[SerializeField] private TaskManager taskManager;
    [FoldoutGroup("Texts"), InfoBox("The texts of the TaskObjectUI are referenced here")]
    [SerializeField] private TextMeshProUGUI taskNameText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI taskDescriptionText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI taskDifficultyText;

    public void SetTaskName(string _taskName)
    {
        taskNameText.text = _taskName;
    }

    public void SetTaskDescription(string _taskDescription)
    {
        taskDescriptionText.text = _taskDescription;
    }

    public void SetTaskDifficulty(int _difficulty)
    {
        taskDifficultyText.text = _difficulty.ToString();
        switch (_difficulty)
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
    }
}
