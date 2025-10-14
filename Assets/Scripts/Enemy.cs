using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private FloatingHealthBar m_floatingHealthBar;
    private int m_currentHealth = 100;
    private int m_maximumHealth = 100;
    private Camera m_mainCamera;
    private Player m_player;

    [SerializeField]
    private float m_enemySpeed;

    void Awake()
    {
        m_floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
        m_mainCamera = Camera.main;
        m_player = Player.Instance;
    }

    void Update()
    {
        // TODO: This code is kind of repetitive to that in the player class, refactor later
        Vector3 enemyScreenPosition = m_mainCamera.WorldToScreenPoint(gameObject.transform.localPosition);
        Vector3 playerScreenPosition = m_mainCamera.WorldToScreenPoint(Player.Instance.gameObject.transform.localPosition);

        float diffX = enemyScreenPosition.x - playerScreenPosition.x;
        float diffY = enemyScreenPosition.y - playerScreenPosition.y;

        float angle = -Mathf.Atan2(diffX, diffY) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float currentX = gameObject.transform.localPosition.x;
        float currentY = gameObject.transform.localPosition.y;

        Vector3 normalizedDirection = (gameObject.transform.localPosition - Player.Instance.gameObject.transform.localPosition).normalized;

        float newX = currentX -= normalizedDirection.x * m_enemySpeed * Time.deltaTime;
        float newY = currentY -= normalizedDirection.y * m_enemySpeed * Time.deltaTime;

        gameObject.transform.localPosition = new Vector3(newX, newY, 0);
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
