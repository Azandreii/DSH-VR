using Newtonsoft.Json.Bson;
//using Sirenix.OdinInspector.Editor.Drawers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnPauseAction;
    public event EventHandler OnAlternateAction;
    public event EventHandler OnSelectTask;
    public event EventHandler<OnTaskCompletedEventArgs> OnTaskCompleted;
    public class OnTaskCompletedEventArgs : EventArgs
    {
        public int totalTasks;
        public TaskSO taskSO;
    }

    private int TasksCompleted = 0;
    private bool isPaused = false;
    private TaskSO selectedTaskSO;
    private GameObject selectedGameObjectTaskSO;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnAlternateAction?.Invoke(this, EventArgs.Empty);
            TogglePause();
        }
    }

    public void AddTaskCompleted(TaskSO _taskSO, GameObject _gameObjectTaskSO)
    {
        OnTaskCompleted?.Invoke(this, new OnTaskCompletedEventArgs
        {
            totalTasks = TasksCompleted,
            taskSO = _taskSO,
        });
        Destroy(_gameObjectTaskSO);
        TasksCompleted++;
        if (selectedTaskSO == _taskSO && selectedTaskSO != null)
        {
            selectedTaskSO = null;
        }
    }

    public void RechargeTaskCompleted(TaskSO _taskSO, GameObject _gameObjectTaskSO)
    {
        OnTaskCompleted?.Invoke(this, new OnTaskCompletedEventArgs
        {
            totalTasks = TasksCompleted,
            taskSO = _taskSO,
        });
        Destroy(_gameObjectTaskSO);
        if (selectedTaskSO == _taskSO && selectedTaskSO != null)
        {
            selectedTaskSO = null;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        switch (isPaused)
        {
            case true:
                Time.timeScale = 0f;
                break;
            case false:
                Time.timeScale = 1f;
                break;
        }
        Debug.Log($"The game has been paused : { isPaused }");
    }

    public bool HasTask()
    {
        return selectedTaskSO != null;
    }

    public void SetTaskSO(TaskSO _taskSO, GameObject _gameObjectTaskSO = null)
    {
        selectedTaskSO = _taskSO;
        selectedGameObjectTaskSO = _gameObjectTaskSO;
        OnSelectTask?.Invoke(this, EventArgs.Empty);
    }

    public TaskSO GetTaskSO()
    {
        return selectedTaskSO;
    }

    public GameObject GetGameObjectTaskSO()
    {
        return selectedGameObjectTaskSO;
    }
}
