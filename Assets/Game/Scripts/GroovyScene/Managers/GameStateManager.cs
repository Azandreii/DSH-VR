using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public static event EventHandler<OnGamestateTutorialEventArgs> OnGamestateTutorial;
    public class OnGamestateTutorialEventArgs
    {
        public int tutorialState;
    }

    public enum GameState
    {
        WaitForPlayers,
        CountToStart,
        Tutorial,
        Playing,
        GameOver,
    }

    [Header("Attributes")]
    [SerializeField] private GameState gameState;
    [SerializeField] private bool isTutorial = false;
    private int tutorialState;
    private bool hasGrabbedPhoneTutorial;
    private bool hasSelectedTaskTutorial;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeClockManager.Instance.OnDayFinished += TimeClockManager_OnDayFinished;
        TaskPhone.Instance.OnPhoneGrabbed += TaskPhone_OnPhoneGrabbed;
        GameManager.Instance.OnSelectTask += GameManager_OnSelectTask;
        if (isTutorial)
        {
            gameState = GameState.Tutorial;
            OnGamestateTutorial?.Invoke(this, new OnGamestateTutorialEventArgs
            {
                tutorialState = tutorialState,
            });
        }
        else
        {
            gameState = GameState.CountToStart;
        }
    }

    private void GameManager_OnSelectTask(object sender, EventArgs e)
    {
        if (gameState == GameState.Tutorial && !hasSelectedTaskTutorial)
        {
            NextTutorialState();
            hasSelectedTaskTutorial = true;
        }
    }

    private void TaskPhone_OnPhoneGrabbed(object sender, System.EventArgs e)
    {
        if (gameState == GameState.Tutorial && !hasGrabbedPhoneTutorial)
        {
            NextTutorialState();
            hasGrabbedPhoneTutorial = true;
        }
    }

    private void TimeClockManager_OnDayFinished(object sender, System.EventArgs e)
    {
        gameState = GameState.GameOver;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitForPlayers:
                gameState = GameState.CountToStart;
                break;
            case GameState.CountToStart:
                break;
            case GameState.Tutorial:
                break;
            case GameState.Playing:
                //Playing state gets changed in the Taskphone script
                break;
            case GameState.GameOver:
                //GameOver state is being set in the TimeClockManager script
                Debug.Log("Game Ended");
                break;
        }
    }

    public void NextTutorialState()
    {
        tutorialState++;
        OnGamestateTutorial?.Invoke(this, new OnGamestateTutorialEventArgs
        {
            tutorialState = tutorialState,
        });
    }

    public void SetGamestate(GameState _gameState)
    {
        if (gameState == GameState.Tutorial)
        {
            tutorialState++;
            OnGamestateTutorial?.Invoke(this, new OnGamestateTutorialEventArgs
            {
                tutorialState = tutorialState,
            });
        }
        gameState = _gameState;
    }

    public bool GetGameStatePlaying()
    {
        return gameState == GameState.Playing;
    }

    public int GetTutorialState()
    {
        return tutorialState;
    }

    public bool GetIsTutorial()
    {
        return isTutorial;
    }
}
