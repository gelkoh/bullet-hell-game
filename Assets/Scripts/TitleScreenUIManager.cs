using UnityEngine;

public class TitleScreenUIManager : MonoBehaviour
{
    public void OnStartGameClicked()
    {
        GameStateManager.StartGame();
    }
}