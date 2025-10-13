using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider m_slider;

    void Awake()
    {
        m_slider = gameObject.GetComponent<Slider>();
    }

    public void UpdateHealth(int currentHealth, int maximumHealth)
    {
        m_slider.value = (float)currentHealth / maximumHealth;
    }
}
