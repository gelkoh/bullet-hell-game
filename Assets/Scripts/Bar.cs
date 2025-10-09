using UnityEngine;

public class Bar : MonoBehaviour
{
    private RectTransform m_rectTransform;
    private float m_maxBarWidth;

    void Awake()
    {
        m_rectTransform = gameObject.GetComponent<RectTransform>();
        m_maxBarWidth = m_rectTransform.rect.width;
        Player.OnHealthChanged += HandleHealthChanged;
    }

    void HandleHealthChanged(int remainingHealthPoints, int maximumHealthPoints)
    {
        float remainingHealthPointsRatio = (float)remainingHealthPoints / maximumHealthPoints;
        float newWidth = m_maxBarWidth * remainingHealthPointsRatio;
        m_rectTransform.sizeDelta = new Vector2(newWidth, m_rectTransform.rect.height);
    }
}
