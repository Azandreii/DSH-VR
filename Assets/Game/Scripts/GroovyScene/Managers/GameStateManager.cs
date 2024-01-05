using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public enum GameState
    {
        WaitForPlayers,
        CountToStart,
        Playing,
        GameOver,
    }

    [Header("Attributes")]
    [SerializeField] private GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeClockManager.Instance.OnDayFinished += TimeClockManager_OnDayFinished;
        TaskPhone.Instance.OnPhoneGrabbed += TaskPhone_OnPhoneGrabbed;
    }

    private void TaskPhone_OnPhoneGrabbed(object sender, System.EventArgs e)
    {
        gameState = GameState.Playing;
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
            case GameState.Playing:
                //Playing state gets changed in the Taskphone script
                break;
            case GameState.GameOver:
                //GameOver state is being set in the TimeClockManager script
                Debug.Log("Game Ended");
                break;
        }
    }

    public bool GetGameStatePlaying()
    {
        return gameState == GameState.Playing;
    }
}
