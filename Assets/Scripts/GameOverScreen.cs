using UnityEngine;
using TMPro;
using System.Text;

public class GameOverScreen : MonoBehaviour
{
    public HighScoreManager HighScoreManager;
    public TMP_Text HighScoresText;
    private CanvasGroup m_canvasGroup;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        Player.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        Player.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver(int finalScore)
    {
        Time.timeScale = 0f;

        if (m_canvasGroup != null)
        {
            m_canvasGroup.alpha = 1f;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }

        if (HighScoreManager == null)
        {
            Debug.LogError("HighScoreManager reference is missing!");
            return;
        }

        HighScoreManager.AddScore("Ole", finalScore);

        var scores = HighScoreManager.GetHighScores();
        if (HighScoresText == null)
        {
            Debug.LogError("HighScores TMP_Text reference is missing!");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("=== Highscores ===");

        for (int i = 0; i < scores.Count; i++)
        {
            var entry = scores[i];
            sb.AppendFormat("{0}. {1} - {2}", i + 1, entry.playerName, entry.score);
            sb.AppendLine();
        }

        HighScoresText.text = sb.ToString();
    }
}