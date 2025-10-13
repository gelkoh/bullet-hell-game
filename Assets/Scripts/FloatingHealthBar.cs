using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider m_slider;

    void Awake()
    {
        m_slider = gameObject.GetComponent<Slider>();
        Enemy.OnDamageTaken += HandleDamageTaken;
    }

    void HandleDamageTaken(int remainingHealthPoints, int maximumHealthPoints)
    {
        Debug.Log("FloatingHealthBar: Handling damage taken.");
        m_slider.value = (float)remainingHealthPoints / maximumHealthPoints;
    }
}
