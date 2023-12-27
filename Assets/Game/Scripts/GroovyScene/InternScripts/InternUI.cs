using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InternObjectUI internObjectUI;

    [Header("Attributes")]
    [SerializeField] private bool isPaused = false;

    private void Start()
    {
        GameManager.Instance.OnPauseAction += GameManager_OnPauseAction;
        UpdatePauseMenu();
    }

    private void GameManager_OnPauseAction(object sender, System.EventArgs e)
    {
        ChangePauseState();
    }

    private void ChangePauseState()
    {
        isPaused = !isPaused;
        UpdatePauseMenu();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdatePauseMenu()
    {
        switch (isPaused)
        {
            case true:
                Show();
                break;
            case false:
                Hide();
                break;
        }
    }
}
