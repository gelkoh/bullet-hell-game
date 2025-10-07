using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();

        GameStateManager.OnStateChange += HandleStateChange;
    }

    void HandleStateChange(GameState newGameState)
    {
        if (newGameState == GameState.TitleScreen)
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }
        else
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
        }
    }
}
