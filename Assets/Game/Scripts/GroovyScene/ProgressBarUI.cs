using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] InternManager internManager;
    [SerializeField] Slider progressBar;
    bool isWorking;

    private void Start()
    {
        internManager.OnStateChanged += InternManager_OnStateChanged;
        internManager.OnProgressChanged += InternManager_OnProgressChanged;
        Hide();
    }

    private void InternManager_OnProgressChanged(object sender, InternManager.OnProgressChangedEventArgs e)
    {
        progressBar.value = e.progressNormalized;
    }

    private void InternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        isWorking = e.state == InternManager.State.WorkingOnTask || e.state == InternManager.State.WaitingForApproval;
        gameObject.SetActive(isWorking);
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
