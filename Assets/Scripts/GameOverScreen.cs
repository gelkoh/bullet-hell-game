using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += HandleStateChange;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= HandleStateChange;
    }

    private void HandleStateChange(GameState newState)
    {
        if (newState == GameState.DeathScreen)
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }
}