using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternObjectUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button assignButton;
    [SerializeField] private TextMeshProUGUI assignText;
    [SerializeField] private TextMeshProUGUI internName;
    [SerializeField] private Button approveButton;
    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider progressBar;
    [SerializeField] private InternManager internManager;

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
        if (internManager.GetInternState() == InternManager.State.Unavailable)
        {
            assignText.color = Color.red;
        }
        else
        {
            assignText.color = Color.white;
        }
    }
}
