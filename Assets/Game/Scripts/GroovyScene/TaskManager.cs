using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public event EventHandler<OnTaskAddedEventArgs> OnTaskAdded;
    public class OnTaskAddedEventArgs : EventArgs
    {
        public bool hasTasks;
        public float difficulty;
    }

    [SerializeField] private TaskSO[] taskArraySO;
    [SerializeField] private Transform taskTemplate;
    [SerializeField] private Transform taskUI;
    [SerializeField] private int taskAmountMax = 2;
    private float timeTillNextTask;
    [SerializeField] private float taskTimerMax = 10f;
    private List<TaskSO> currentTasksList;
    private List<Transform> currentTasksObjectList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentTasksList = new List<TaskSO>();
        currentTasksObjectList = new List<Transform>();
        taskTemplate.gameObject.SetActive(false);
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (currentTasksList.Count > 0)
        {
            Destroy(currentTasksObjectList[0].gameObject);
            currentTasksList.Remove(currentTasksList[0]);
            currentTasksObjectList.Remove(currentTasksObjectList[0]);
        }
        if (currentTasksList.Count == 0)
        {
            OnTaskAdded?.Invoke(this, new OnTaskAddedEventArgs
            {
                hasTasks = false
            });
        }
    }

    private void Update()
    {
        Debug.Log(currentTasksList.Count);
        timeTillNextTask -= Time.deltaTime;
        if (timeTillNextTask < 0)
        {
            timeTillNextTask = taskTimerMax;
            CreateTask(taskArraySO[UnityEngine.Random.Range(0, taskArraySO.Length)]);
        }
    }

    private void CreateTask(TaskSO _TaskSO)
    {
        if (currentTasksList.Count < taskAmountMax)
        {
            Transform _taskObjectUI = Instantiate(taskTemplate, taskUI);
            TaskObjectUI toUI = _taskObjectUI.GetComponent<TaskObjectUI>();
            toUI.SetTaskName(GetTaskName(_TaskSO));
            toUI.SetTaskDescription(GetTaskDescription(_TaskSO));
            toUI.SetTaskDifficulty(GetTaskDifficulty(_TaskSO));
            _taskObjectUI.gameObject.SetActive(true);
            currentTasksList.Add(_TaskSO);
            currentTasksObjectList.Add(_taskObjectUI);
            OnTaskAdded?.Invoke(this, new OnTaskAddedEventArgs
            {
                hasTasks = true,
                difficulty = GetTaskDifficulty(_TaskSO)
            });
        }
    }

    private Transform GetTaskObjectUI(TaskSO _taskSO)
    {
        return _taskSO.taskObjectUI;
    }

    private string GetTaskName(TaskSO _taskSO)
    {
        return _taskSO.taskName;
    }

    private string GetTaskDescription(TaskSO _taskSO)
    {
        return _taskSO.taskDescription;
    }

    private int GetTaskDifficulty(TaskSO _taskSO)
    {
        return _taskSO.taskDifficulty;
    }

    public bool HasTasks()
    {
        return currentTasksList.Count > 0;
    }

    public int GetCurrentTaskDifficulty()
    {
        return GetTaskDifficulty(currentTasksList[0]);
    }
}
