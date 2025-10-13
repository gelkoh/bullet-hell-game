using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction m_moveAction;
    private InputAction m_attackAction;

    private Camera m_mainCamera;
    private Transform m_mainCameraTransform;

    private int m_maximumHealthPoints = 100;
    private int m_remainingHealthPoints = 100;

    public static event Action<int, int> OnHealthChange;

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
        m_rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        
        m_moveAction = InputSystem.actions.FindAction("Player/Move");
        m_attackAction = InputSystem.actions.FindAction("Player/Attack");

        m_attackAction.performed += HandleAttackAction;

        m_mainCamera = Camera.main;
        m_mainCameraTransform = m_mainCamera.GetComponent<Transform>();
    }

    void Start()
    {
        // Test the health bar
        RemainingHealthPoints = 75;
    }

    void Update()
    {
        Vector2 moveValue = m_moveAction.ReadValue<Vector2>();

        float currentX = gameObject.transform.localPosition.x;
        float currentY = gameObject.transform.localPosition.y;

        // Multiply position by time scale so player movement stops while in menu/while pausing
        float newX = currentX += moveValue.x * 0.01f * Time.timeScale;
        float newY = currentY += moveValue.y * 0.01f * Time.timeScale;

        gameObject.transform.localPosition = new Vector3(newX, newY, 0);

        // Update camera position to track player position
        m_mainCameraTransform.position = new Vector3(newX, newY, -10);

        Vector3 playerScreenPosition = m_mainCamera.WorldToScreenPoint(gameObject.transform.localPosition);
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        float diffX = mouseScreenPosition.x - playerScreenPosition.x;
        float diffY = mouseScreenPosition.y - playerScreenPosition.y;

        float angle = -Mathf.Atan2(diffX, diffY) * Mathf.Rad2Deg + 90f;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(m_laserBulletPrefab, m_rigidBody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        LaserBullet laserBullet = projectileObject.GetComponent<LaserBullet>();

        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = m_mainCamera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, m_mainCamera.nearClipPlane));

        Vector2 mouseWorldPosition2D = mouseWorldPosition;
        Vector2 playerWorldPosition2D = gameObject.transform.localPosition;

        Vector2 bulletDirection = mouseWorldPosition2D - playerWorldPosition2D;
        Vector2 normalizedBulletDirection = bulletDirection.normalized;

        laserBullet.Launch(normalizedBulletDirection, 1000);
    }

    void HandleAttackAction(InputAction.CallbackContext context)
    {
        Launch();
    }
}
