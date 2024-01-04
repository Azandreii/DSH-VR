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
        InputManagerVR.Instance.OnTrigger += InputManagerVR_OnTrigger;
    }

    private void InputManagerVR_OnTrigger(object sender, InputManagerVR.OnTriggerEventArgs e)
    {
        selectedTaskSO = null;
        UpdateVisual();
    }

    private void GameManager_OnSelectTask(object sender, System.EventArgs e)
    {
        selectedTaskSO = GameManager.Instance.GetTaskSO();
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (selectedTaskSO == null && hasSpawned == true)
        {
            selectedManagerTaskSO.DestroySelf();
            selectedTaskBackside.gameObject.SetActive(false);
            hasSpawned = false;
        }
        else if (selectedTaskSO != null)
        {
            Transform _spawnedTaskUI = Instantiate(selectedTaskSO.taskSelectedUI, selectedTaskSpawnPoint);
            selectedManagerTaskSO = _spawnedTaskUI.GetComponent<SelectedManagerTaskSO>();
            selectedManagerTaskSO.SetSelectedTaskSO(selectedTaskSO);
            selectedTaskBackside.gameObject.SetActive(true);
            hasSpawned = true;
        }
    }
}
