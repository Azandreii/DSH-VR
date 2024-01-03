using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTaskSO : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image imageSelectedTaskSO;
    [SerializeField] private TextMeshProUGUI textSelectedTaskSO;
    private TaskSO selectedTaskSO;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnSelectTask += Instance_OnSelectTask;
        InputManagerVR.Instance.OnTrigger += InputManagerVR_OnTrigger;
    }

    private void InputManagerVR_OnTrigger(object sender, InputManagerVR.OnTriggerEventArgs e)
    {
        Debug.Log("InputManagerVR_OnTrigger");
        selectedTaskSO = null;
        UpdateVisual();
    }

    private void Instance_OnSelectTask(object sender, System.EventArgs e)
    {
        Debug.Log("Instance_OnSelectTask");
        selectedTaskSO = GameManager.Instance.GetTaskSO();
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (selectedTaskSO == null)
        {
            Debug.Log("UpdateVisual: Hide");
            Hide();
        }
        else if (selectedTaskSO != null)
        {
            Debug.Log("UpdateVisual: Show");
            Show();
            textSelectedTaskSO.text = selectedTaskSO.taskName;
        }
    }

    private void Hide()
    {
        imageSelectedTaskSO.gameObject.SetActive(false);
    }

    private void Show()
    {
        imageSelectedTaskSO.gameObject.SetActive(true);
    }
}
