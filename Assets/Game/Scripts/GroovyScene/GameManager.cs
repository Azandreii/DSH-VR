using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnPauseAction;
    public event EventHandler OnAlternateAction;
    public event EventHandler<OnTaskCompletedEventArgs> OnTaskCompleted;
    public class OnTaskCompletedEventArgs : EventArgs
    {
        public int totalTasks;
    }

    private List<Transform> internList;
    private int TasksCompleted = 0;
    private bool isPaused = false;

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

    public void AddTaskCompleted()
    {
        TasksCompleted++;
        OnTaskCompleted?.Invoke(this, new OnTaskCompletedEventArgs
        {
            totalTasks = TasksCompleted
        }) ;
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
    }
}
