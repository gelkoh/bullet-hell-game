using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private FloatingHealthBar m_floatingHealthBar;
    private int m_currentHealth = 100;
    private int m_maximumHealth = 100;

    void Awake()
    {
        m_floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            m_floatingHealthBar.UpdateHealth(m_currentHealth, m_maximumHealth);
            Debug.Log(gameObject.name + ": Enemy took damage", gameObject);
        }
    }
}
