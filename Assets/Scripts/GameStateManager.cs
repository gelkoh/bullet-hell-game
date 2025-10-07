using System;
using UnityEngine;

public enum GameState
{
    TitleScreen,
    Playing
}

public class GameStateManager : MonoBehaviour
{
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



    // private State m_currentState;

    // void Awake() {
    //     m_currentState = State.TitleScreen;
    // }
    //
    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("Game started!");
        Debug.Log(CurrentState);
        // m_currentState = State.Playing;
    }
}
