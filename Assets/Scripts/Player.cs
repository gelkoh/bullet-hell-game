using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private float m_playerSpeed;

    private InputAction m_moveAction;
    private InputAction m_attackAction;

    [SerializeField]
    private Camera m_mainCamera;

    [SerializeField]
    private Camera m_uiCamera;

    private Transform m_mainCameraTransform;
    private Transform m_uiCameraTransform;

    private Vector2 m_moveDirection;

    private int m_maximumHealthPoints = 100;
    private int m_remainingHealthPoints = 100;

    private int m_score;
    private float m_angle;
    
    public static event Action<int, int> OnHealthChange;
    public static event Action<int> OnScoreChange;
    public static event Action<int> OnGameOver;

    public int MaximumHealthPoints
    {
        get => m_maximumHealthPoints;
        private set
        {
            m_maximumHealthPoints = value;
        }
    }

    public int RemainingHealthPoints
    {
        get => m_remainingHealthPoints; 
        private set
        {
            m_remainingHealthPoints = value;
            OnHealthChange?.Invoke(m_remainingHealthPoints, m_maximumHealthPoints);
        }
    }

    [SerializeField]
    private GameObject m_laserBulletPrefab;

    private Rigidbody2D m_rigidBody2d;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        m_rigidBody2d = gameObject.GetComponent<Rigidbody2D>();

        m_moveAction = InputSystem.actions.FindAction("Player/Move");
        m_attackAction = InputSystem.actions.FindAction("Player/Attack");

        m_mainCameraTransform = m_mainCamera.GetComponent<Transform>();
        m_uiCameraTransform = m_uiCamera.GetComponent<Transform>();
    }

    void OnEnable()
    {
        m_attackAction.performed += HandleAttackAction;
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    void Update()
    {
        m_moveDirection = m_moveAction.ReadValue<Vector2>();
        Vector2 currentMousePosition = Mouse.current.position.ReadValue();

        const float Z_DISTANCE_FROM_CAMERA = 10f;

        Vector3 mouseWorldPosition = m_uiCamera.ScreenToWorldPoint(new Vector3(
            currentMousePosition.x,
            currentMousePosition.y,
            Z_DISTANCE_FROM_CAMERA
        ));

        Vector3 direction = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void LateUpdate()
    {
        m_mainCameraTransform.position = new Vector3(transform.position.x,
                                                     transform.position.y,
                                                     m_mainCameraTransform.position.z);
        m_uiCameraTransform.position = new Vector3(transform.position.x,
                                                     transform.position.y,
                                                     m_uiCameraTransform.position.z);
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = m_rigidBody2d.position;

        Vector2 targetPosition = currentPosition + m_moveDirection * m_playerSpeed * Time.fixedDeltaTime;

        m_rigidBody2d.MovePosition(targetPosition);
    }

    private void Launch()
    {
        Vector3 spawnPositionOffset = new Vector3(0.4f, -0.15f, 0f);
        Vector3 spawnRotationOffset = transform.rotation * spawnPositionOffset;
        Vector3 spawnPosition = transform.position + spawnRotationOffset;
        
        GameObject projectileObject = Instantiate(m_laserBulletPrefab, spawnPosition, Quaternion.Euler(new Vector3(0, 0, m_angle + 90f)));

        LaserBullet laserBullet = projectileObject.GetComponent<LaserBullet>();

        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = m_uiCamera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, m_mainCamera.nearClipPlane));

        Vector2 mouseWorldPosition2D = mouseWorldPosition;
        Vector2 playerWorldPosition2D = gameObject.transform.localPosition;

        Vector2 bulletDirection = mouseWorldPosition2D - playerWorldPosition2D;
        Vector2 normalizedBulletDirection = bulletDirection.normalized;

        laserBullet.Launch(normalizedBulletDirection, 1000);

        AudioManager.Instance.PlayLaserShot();
    }

    void OnDisable()
    {
        m_attackAction.performed -= HandleAttackAction;
        Enemy.OnEnemyKilled -= HandleEnemyKilled; 
    }

    void HandleAttackAction(InputAction.CallbackContext context)
    {
        if (GameStateManager.CurrentState == GameState.Playing)
        {
            Launch();
        }
    }

    public void TakeDamage(int damage)
    {
        RemainingHealthPoints -= damage;

        if (RemainingHealthPoints <= 0)
        {
            GameStateManager.SetState(GameState.DeathScreen);
            OnGameOver?.Invoke(m_score);
        }
    }

    public void Heal(int health)
    {
        if (RemainingHealthPoints + health <= 100)
        {
            RemainingHealthPoints += health;
        }
        else
        {
            RemainingHealthPoints = 100;
        }
    }

    private void HandleEnemyKilled(int points)
    {
        m_score += points;
        OnScoreChange?.Invoke(m_score);
    }
}
