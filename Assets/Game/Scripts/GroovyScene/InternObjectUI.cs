using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InternObjectUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private InternManager internManager;
    [FoldoutGroup("Buttons"), InfoBox("The buttons of the InternObjectUI are referenced here")]
    [SerializeField] private Button assignButton;
    [FoldoutGroup("Texts"), InfoBox("The texts of the InternObjectUI are referenced here")]
    [SerializeField] private TextMeshProUGUI assignText;
    [FoldoutGroup("Texts")]
    [SerializeField] private TextMeshProUGUI internName;
    [FoldoutGroup("Buttons")]
    [SerializeField] private Button approveButton;
    [FoldoutGroup("Sliders"), InfoBox("The sliders of the InternObjectUI are referenced here")]
    [SerializeField] private Slider energyBar;
    [FoldoutGroup("Sliders")]
    [SerializeField] private Slider progressBar;
    [FoldoutGroup("GameObjects"), InfoBox("The gameObject references are referenced here")]
    [SerializeField] private GameObject progressBarObject;

    //[Header("Attributes")]

    private void Start()
    {
        assignButton.onClick.AddListener(() =>
        {
            if (internManager.GetInternState() == InternManager.State.Available)
            {
                internManager.SetTask(InternManager.State.WorkingOnTask, 3f);
            }
        });
        approveButton.onClick.AddListener(() =>
        {
            if (internManager.GetInternState() == InternManager.State.WaitingForApproval)
            {
                internManager.PlayerApproved();
                GameManager.Instance.AddTaskCompleted();
            }
        });

        internManager.OnStateChanged += InternManager_OnStateChanged;
    }


    public void SetName(string _name)
    {
        internName.text = _name;
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        switch (e.state)
        {
            case InternManager.State.Available:
                assignText.color = Color.white;
                progressBarObject.SetActive(false);
                break;
            case InternManager.State.WorkingOnTask:
                assignText.color = Color.white;
                progressBarObject.SetActive(true);
                break;
            case InternManager.State.WaitingForApproval:
                assignText.color = Color.white;
                progressBarObject.SetActive(true);
                break;
            case InternManager.State.Unavailable:
                assignText.color = Color.red;
                progressBarObject.SetActive(false);
                break;
        }
    }
}
