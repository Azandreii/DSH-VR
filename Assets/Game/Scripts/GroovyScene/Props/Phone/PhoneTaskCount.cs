using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTaskCount : MonoBehaviour
{
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
        phoneTaskCountText.text = "0";
        UpdateVisual();
        TaskManager.Instance.OnTaskAdded += TaskManager_OnTaskAdded;
    }

    private void TaskManager_OnTaskAdded(object sender, TaskManager.OnTaskAddedEventArgs e)
    {
        phoneTaskCount = Mathf.RoundToInt(e.taskAmount);
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (phoneTaskCount == 0 && PhoneManager.Instance.IsPhoneMenu() && !TaskPhone.Instance.GetInPants())
        {
            phoneTaskCountImage.gameObject.SetActive(false);
        }
        else if (phoneTaskCount > 0 && PhoneManager.Instance.IsPhoneMenu() && !TaskPhone.Instance.GetInPants())
        {
            phoneTaskCountImage.gameObject.SetActive(true);
            phoneTaskCountText.text = phoneTaskCount.ToString();
        }
    }
}
