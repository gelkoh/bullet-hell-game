using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private Transform m_enemyBody;

    [SerializeField]
    private float m_enemySpeed;

    private FloatingHealthBar m_floatingHealthBar;
    private CanvasGroup m_canvasGroup;
    private Camera m_mainCamera;
    private Player m_player;
    private Rigidbody2D m_enemyRigidBody;
    private Rigidbody2D m_playerRigidBody;

    private int m_currentHealth = 100;
    private int m_maximumHealth = 100;

    private Vector3 m_healthBarOffset = new Vector3(0f, -0.5f, 0f);

    public static event Action<int> OnEnemyKilled;

    void Awake()
    {
        m_floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
        m_canvasGroup = GetComponentInChildren<CanvasGroup>();
        m_mainCamera = Camera.main;
        m_player = Player.Instance;

        m_enemyRigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 enemyPosition = m_enemyRigidBody.position;
        Vector2 playerPosition = Player.Instance.gameObject.GetComponent<Rigidbody2D>().position;

        Vector2 difference = playerPosition - enemyPosition;

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;
        m_enemyBody.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FixedUpdate()
    {
        Vector2 enemyPosition = m_enemyRigidBody.position;
        Vector2 playerPosition = Player.Instance.gameObject.GetComponent<Rigidbody2D>().position;

        Vector2 difference = playerPosition - enemyPosition;

        Vector2 normalizedDirection = difference.normalized;

        Vector2 newPosition = enemyPosition + normalizedDirection * m_enemySpeed * Time.deltaTime;

        m_enemyRigidBody.MovePosition(newPosition);
    }

    void LateUpdate()
    {
        m_floatingHealthBar.transform.position = m_enemyBody.position + m_healthBarOffset;
        m_floatingHealthBar.transform.rotation = Quaternion.identity;
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth < m_maximumHealth)
        {
            m_canvasGroup.alpha = 1;
        }

        if (m_currentHealth <= 0)
        {
            OnEnemyKilled?.Invoke(100);
            Destroy(gameObject);
        }
        else
        {
            m_floatingHealthBar.UpdateHealth(m_currentHealth, m_maximumHealth);
            Debug.Log(gameObject.name + ": Enemy took damage", gameObject);
        }
    }
}
