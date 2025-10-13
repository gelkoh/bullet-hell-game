using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private int m_currentHealth = 100;
    private int m_maximumHealth = 100;
    public static event Action<int, int> OnDamageTaken;

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy: Enemy was killed!");
        }
        else
        {
            OnDamageTaken?.Invoke(m_currentHealth, m_maximumHealth);
            Debug.Log("Enemy: Taken damage");
        }
    }
}
