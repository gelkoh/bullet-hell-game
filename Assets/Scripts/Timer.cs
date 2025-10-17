using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text m_timerText;
    private float elapsedTime = 0f;

    void Awake()
    {
        m_timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1.0f)
        {
            elapsedTime = 0.0f;

            int seconds = (int)Time.time % 60;
            int minutes = (int)Time.time / 60;

            m_timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
}