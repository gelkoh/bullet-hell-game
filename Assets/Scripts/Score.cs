using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text m_text;

    void Awake()
    {
        m_text = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        Player.OnScoreChange += HandleScoreChange;
    }

    void OnDisable()
    {
        Player.OnScoreChange -= HandleScoreChange; 
    }

    private void HandleScoreChange(int newScore)
    {
        m_text.SetText(newScore.ToString());
    }
}
