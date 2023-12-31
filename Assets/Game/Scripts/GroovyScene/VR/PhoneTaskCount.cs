using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTaskCount : MonoBehaviour
{
    //Change the Task Manager event from has tasks to task count.
    //With it, you can get the amount of tasks and use that to update the task count, instead of 
    //creatung a seperate script and gameObject in the scene
    public static PhoneTaskCount Instance;

    [Header("References")]
    [SerializeField] private Image phoneTaskCountImage;
    [SerializeField] private TextMeshProUGUI phoneTaskCountText;
    private int phoneTaskCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TaskManager.Instance.OnTaskAdded += TaskManager_OnTaskAdded;
    }

    private void TaskManager_OnTaskAdded(object sender, TaskManager.OnTaskAddedEventArgs e)
    {
        phoneTaskCount = Mathf.RoundToInt(e.taskAmount);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (phoneTaskCount == 0)
        {
            phoneTaskCountImage.gameObject.SetActive(false);
        }
        else
        {
            phoneTaskCountImage.gameObject.SetActive(true);
            phoneTaskCountText.text = phoneTaskCount.ToString();
        }
    }
}
