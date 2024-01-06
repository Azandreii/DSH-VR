using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjectVR : MonoBehaviour
{
    [Header("Refereces")]
    [SerializeField] private InternManager tutorialInternManager;
    [SerializeField] private ButtonVR buttonTutorial;
    [SerializeField] private OnCollisionVR collisionTriggerTutorial;
    [SerializeField] private GameObject tutorialObjectReference;

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
        GameStateManager.OnGamestateTutorial += GameStateManager_OnGamestateTutorial;
        if (buttonTutorial != null)
        {
            buttonTutorial.OnClick += ButtonTutorial_OnClick;
        }
        if (collisionTriggerTutorial != null)
        {
            collisionTriggerTutorial.OnCollisionControler += CollisionTriggerTutorial_OnCollisionControler;
        }
    }

    private void CollisionTriggerTutorial_OnCollisionControler(object sender, System.EventArgs e)
    {
        if (tutorialInternManager != null)
        {
            if (tutorialInternManager.GetInternState() == InternManager.InternState.WaitingForApproval)
            {
                AdjustTutorial();
            }
        }
        else if (tutorialInternManager == null)
        {
            AdjustTutorial();
        }
    }

    private void ButtonTutorial_OnClick(object sender, ButtonVR.OnClickEventArgs e)
    {
        if (e.clickState == ButtonVR.ClickState.ClickDown)
        {
            AdjustTutorial();
        }
    }

    private void GameStateManager_OnGamestateTutorial(object sender, GameStateManager.OnGamestateTutorialEventArgs e)
    {
        switch (showDuringTutorial)
        {
            case true:
                Show();
                if (GameStateManager.Instance.GetGameStatePlaying())
                {
                    Hide();
                }
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
