using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TotalTasksUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI totalTasksText;

    private void Start()
    {
        GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        totalTasksText.text = e.totalTasks.ToString();
    }
}
