using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternStateUI : MonoBehaviour
{
    [SerializeField] InternManager internManager;
    [SerializeField] TextMeshProUGUI internStateText;
    [SerializeField] Image internStateBackground;

    private void Start()
    {
        internManager.OnStateChanged += InternManager_OnStateChanged;
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        switch (e.internState)
        {
            case InternManager.InternState.Idle:
                internStateText.color = Color.white;
                internStateText.text = "Idle";
                break;
            case InternManager.InternState.WorkingOnTask:
                internStateText.color = Color.yellow;
                internStateText.text = "Working";
                break;
            case InternManager.InternState.WaitingForApproval:
                internStateText.color = Color.cyan;
                internStateText.text = "Finished";
                break;
            case InternManager.InternState.Unavailable:
                internStateText.color = Color.red;
                internStateText.text = "Unavailable";
                break;
        }
    }
}
