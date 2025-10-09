using System;
using UnityEngine;

public enum GameState
{
    TitleScreen,
    Playing,
    Menu
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameplayRoot;

    public static event Action<GameState> OnStateChange;
    private static GameState m_currentState;

    public static GameState CurrentState
    {
        get => m_currentState;
        private set
        {
            m_currentState = value;
            OnStateChange?.Invoke(m_currentState);
        }
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("GameStateManager: Game started!");
        m_gameplayRoot.SetActive(true);
    }

    public static void ToggleMenu()
    {
        if (CurrentState == GameState.Menu)
        {
            CurrentState = GameState.Playing;
        }
        else
        {
            CurrentState = GameState.Menu;
        }
    }

    public void EndGame()
    {
        CurrentState = GameState.TitleScreen;
        Debug.Log("GameStateManager: Ended the game");
        m_gameplayRoot.SetActive(false);
    }
}
