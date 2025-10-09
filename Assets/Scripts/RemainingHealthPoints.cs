using System;
using UnityEngine;
using TMPro;

public class RemainingHealthPoints : MonoBehaviour
{
    private TMP_Text m_healthPointsText;

    void Awake()
    {
        m_healthPointsText = gameObject.GetComponent<TMP_Text>();
        Player.OnHealthChange += HandleHealthChange;
    }

    void HandleHealthChange(int remainingHealthPoints, int maximumHealthPoints)
    {
        m_healthPointsText.text = remainingHealthPoints.ToString();
    }
}
