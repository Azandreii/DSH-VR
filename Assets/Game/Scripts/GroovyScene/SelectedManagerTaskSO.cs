using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedManagerTaskSO : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image imageSelectedTaskSO;
    [SerializeField] private TextMeshProUGUI textSelectedTaskUI;

    private void Awake()
    {
        Hide();
    }

    public void SetSelectedTaskSO(TaskSO _taskSO)
    {
        textSelectedTaskUI.text = _taskSO.taskName;
        Show();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Hide()
    {
        imageSelectedTaskSO.gameObject.SetActive(false);
        textSelectedTaskUI.gameObject.SetActive(false);
    }

    public void Show()
    {
        imageSelectedTaskSO.gameObject.SetActive(true);
        textSelectedTaskUI.gameObject.SetActive(true);
    }
}
