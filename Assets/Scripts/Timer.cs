using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text m_timerText;
    private float m_elapsedTime = 0f;

    void Awake()
    {
        m_timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        m_elapsedTime += Time.deltaTime;

        int seconds = (int)m_elapsedTime % 60;
        int minutes = (int)m_elapsedTime / 60;

        m_timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
