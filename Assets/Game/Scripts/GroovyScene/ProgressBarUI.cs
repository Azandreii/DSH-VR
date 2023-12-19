using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] InternManager internManager;
    [SerializeField] Slider progressBar;

    private void Start()
    {
        internManager.OnProgressChanged += InternManager_OnProgressChanged;
        Hide();
    }

    private void InternManager_OnProgressChanged(object sender, InternManager.OnProgressChangedEventArgs e)
    {
        progressBar.value = e.progressNormalized;
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
