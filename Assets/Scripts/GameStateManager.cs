using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    TitleScreen,
    Playing,
    Menu
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

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

    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
        CurrentState = GameState.Playing;
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

    public static void ResumeGame()
    {
        CurrentState = GameState.Playing;
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("TitleScreenScene");
        CurrentState = GameState.TitleScreen;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
