using UnityEngine;

public class Menu : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();

        GameStateManager.OnStateChange += HandleStateChange;
    }

    void HandleStateChange(GameState newGameState)
    {
        m_canvasGroup.alpha = 0;
        Debug.Log("Handling state change");
    }
}
