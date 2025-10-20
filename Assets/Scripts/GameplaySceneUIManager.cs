using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void OnBackToTitleScreenClicked()
    {
        GameStateManager.EndGame();
    }
}
