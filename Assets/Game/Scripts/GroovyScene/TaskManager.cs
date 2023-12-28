using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public event EventHandler OnTaskAdded;
    public class OnTaskAddedEventArgs : EventArgs
    {
        public bool hasTasks;
        public float difficulty;
    }

    [SerializeField] private TaskSO[] taskArraySO;
    [SerializeField] private Transform taskTemplate;
    [SerializeField] private Transform taskUI;
    [SerializeField] private float taskAmountMax = 2f; //0, 1, 2, which means 3
    private float timeTillNextTask;
    [SerializeField] private float taskTimerMax = 10f;
    private List<TaskSO> currentTasksList;

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
        currentTasksList = new List<TaskSO>();
    }

    private void Start()
    {
        taskTemplate.gameObject.SetActive(false);
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (currentTasksList.Count > 0)
        {
            currentTasksList.Remove(e.taskSO);
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
        if (currentTasksList.Count < taskAmountMax)
        {
            timeTillNextTask -= Time.deltaTime;
            if (timeTillNextTask < 0)
            {
                timeTillNextTask = taskTimerMax;
                CreateTask(RandomTask());
            }
        }
    }

    private void CreateTask(TaskSO _TaskSO)
    {
        if (currentTasksList.Count < taskAmountMax)
        {
            Transform _taskObjectUI = Instantiate(taskTemplate, taskUI);
            TaskObjectUI toUI = _taskObjectUI.GetComponent<TaskObjectUI>();
            toUI.SetTaskSO(_TaskSO);
            
            currentTasksList.Add(_TaskSO);
            OnTaskAdded?.Invoke(this, new OnTaskAddedEventArgs
            {
                hasTasks = true,
                difficulty = GetTaskDifficulty(_TaskSO)
            });
        }
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

    private TaskSO RandomTask()
    {
        List<TaskSO> _availableTaskSOList = new List<TaskSO>();

        foreach (TaskSO _taskSO in taskArraySO)
        {
            _availableTaskSOList.Add(_taskSO);
        }

        foreach (TaskSO _availableTaskSO in taskArraySO)
        {
            foreach (TaskSO _listTaskSO in currentTasksList)
            {
                if (_availableTaskSO == _listTaskSO)
                {
                    _availableTaskSOList.Remove(_availableTaskSO);
                }
            }
        }
        TaskSO _randomTaskSO = _availableTaskSOList[UnityEngine.Random.Range(0, _availableTaskSOList.Count)];
        _availableTaskSOList.Clear();
        return _randomTaskSO;
    }
}
