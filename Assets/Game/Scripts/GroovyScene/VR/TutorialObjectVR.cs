using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialObjectVR : MonoBehaviour
{
    [Header("Refereces")]
    [SerializeField] private InternManager tutorialInternManager;
    [SerializeField] private GameObject tutorialObjectReference;
    [SerializeField] private ButtonVR tutorialButtonVR;

    [Header("Attributes")]
    [SerializeField] private int showOnTutorialState = 0;
    [SerializeField] private bool showDuringTutorial = false;
    [SerializeField] private bool setGamestatePlaying = false;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        if (GameStateManager.Instance.GetIsTutorial())
        {
            GameStateManager.OnGamestateTutorial += GameStateManager_OnGamestateTutorial;
            GameManager.Instance.OnTaskCompleted += GameManager_OnTaskCompleted;
            tutorialInternManager.OnStateChanged += TutorialInternManager_OnStateChanged;
            if (tutorialButtonVR != null)
            {
                tutorialButtonVR.OnClick += ButtonVR_OnClick;
            }
        }
    }

    private void CollisionVR_OnCollisionControler(object sender, System.EventArgs e)
    {
        AdjustTutorial();
    }

    private void ButtonVR_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            AdjustTutorial();
        }
    }

    private void OnDestroy()
    {
        GameStateManager.OnGamestateTutorial -= GameStateManager_OnGamestateTutorial;
        GameManager.Instance.OnTaskCompleted -= GameManager_OnTaskCompleted;
        tutorialInternManager.OnStateChanged -= TutorialInternManager_OnStateChanged;
        if (tutorialButtonVR != null)
        {
            tutorialButtonVR.OnClick -= ButtonVR_OnClick;
        }
    }

    private void GameManager_OnTaskCompleted(object sender, GameManager.OnTaskCompletedEventArgs e)
    {
        GameStateManager.Instance.SetGamestate(GameStateManager.GameState.Playing);
        Destroy(gameObject);
    }

    private void TutorialInternManager_OnStateChanged(object sender, InternManager.OnStateChangedEventArgs e)
    {
        if (tutorialInternManager.GetInternState() == InternManager.InternState.WaitingForApproval)
        {
            AdjustTutorial();
        }
    }

    private void GameStateManager_OnGamestateTutorial(object sender, GameStateManager.OnGamestateTutorialEventArgs e)
    {
        switch (showDuringTutorial)
        {
            case true:
                if (GameStateManager.Instance.IsGamePlaying())
                {
                    Hide();
                    break;
                }
                Show();
            break;
            case false:
                if (e.tutorialState == showOnTutorialState)
                {
                    Show();
                }
                else if (e.tutorialState != showOnTutorialState)
                {
                    Hide();
                }
            break;
        }
        if (!GameStateManager.Instance.GetIsTutorial())
        {
            Destroy(gameObject);
        }
    }

    private void Show()
    {
        tutorialObjectReference.SetActive(true);
    }

    private void Hide()
    {
        tutorialObjectReference.SetActive(false);
    }

    private void AdjustTutorial()
    {
        if (setGamestatePlaying)
        {
            GameStateManager.Instance.SetGamestate(GameStateManager.GameState.Playing);
        }
        else if (GameStateManager.Instance.GetTutorialState() == showOnTutorialState && !showDuringTutorial)
        {
            GameStateManager.Instance.NextTutorialState();
        }
    }
}
