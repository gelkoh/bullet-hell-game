using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;
    private InputAction m_menuAction;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();

        m_menuAction = InputSystem.actions.FindAction("UI/Menu");
        m_menuAction.performed += ToggleMenu;

        GameStateManager.OnStateChange += HandleStateChange;
    }

    void ToggleMenu(InputAction.CallbackContext context)
    {
        if (GameStateManager.CurrentState == GameState.Playing || 
            GameStateManager.CurrentState == GameState.Menu)
        {
            Debug.Log(gameObject.name + ": Menu toggled");
            GameStateManager.ToggleMenu();
        }
    }

    void HandleStateChange(GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
        }
        else
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            Time.timeScale = 1;
        }
    }
}
