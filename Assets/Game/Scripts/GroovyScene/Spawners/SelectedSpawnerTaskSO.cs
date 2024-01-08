using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedSpawnerTaskSO : MonoBehaviour
{
    public static SelectedSpawnerTaskSO Instance;

    [Header("References")]
    [SerializeField] private Transform selectedTaskSpawnPoint;
    [SerializeField] private Transform selectedTaskPrefab;
    [SerializeField] private Transform selectedTaskBackside;
    private SelectedManagerTaskSO selectedManagerTaskSO;
    private TaskSO selectedTaskSO;
    private bool hasSpawned;

    private void Awake()
    {
        selectedTaskBackside.gameObject.SetActive(false);
        Instance = this;
        selectedTaskPrefab.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnSelectTask += GameManager_OnSelectTask;
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        if (selectedTaskSO == GameManager.Instance.GetTaskSO())
        {
            selectedTaskSO = null;
            UpdateVisual();
        }
    }

    private void GameManager_OnSelectTask(object sender, System.EventArgs e)
    {
        if (selectedTaskSO == null || selectedTaskSO != GameManager.Instance.GetTaskSO())
        {
            selectedTaskSO = GameManager.Instance.GetTaskSO();
            Debug.Log(selectedTaskSO);
            UpdateVisual();
        }
        else if (selectedTaskSO == GameManager.Instance.GetTaskSO())
        {
            GameManager.Instance.SetTaskSO(null);
            selectedTaskSO = null;
            Debug.Log(selectedTaskSO);
            UpdateVisual();
        }
    }

    public void UpdateVisual()
    {
        switch (hasSpawned)
        {
            case true:
                if (selectedTaskSO == null)
                {
                    selectedManagerTaskSO.DestroySelf();
                    selectedManagerTaskSO = null;
                    selectedTaskBackside.gameObject.SetActive(false);
                    hasSpawned = false;
                }
                if (selectedTaskSO != null)
                {
                    selectedManagerTaskSO.DestroySelf();
                    selectedManagerTaskSO = null;
                    Transform _spawnedTaskUI = Instantiate(selectedTaskSO.taskSelectedUI, selectedTaskSpawnPoint);
                    selectedManagerTaskSO = _spawnedTaskUI.GetComponent<SelectedManagerTaskSO>();
                    selectedManagerTaskSO.SetSelectedTaskSO(selectedTaskSO);
                    selectedTaskBackside.gameObject.SetActive(true);
                    hasSpawned = true;
                }
                break;
            case false:
                if (selectedTaskSO != null)
                {
                    Transform _spawnedTaskUI = Instantiate(selectedTaskSO.taskSelectedUI, selectedTaskSpawnPoint);
                    selectedManagerTaskSO = _spawnedTaskUI.GetComponent<SelectedManagerTaskSO>();
                    selectedManagerTaskSO.SetSelectedTaskSO(selectedTaskSO);
                    selectedTaskBackside.gameObject.SetActive(true);
                    hasSpawned = true;
                }
            break;
        }
    }
}
