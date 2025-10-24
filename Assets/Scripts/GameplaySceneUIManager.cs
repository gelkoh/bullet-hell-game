using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void OnBackToTitleScreenClicked()
    {
        GameStateManager.EndGame();
    }

    public void OnResumeClicked()
    {
        GameStateManager.ResumeGame();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
