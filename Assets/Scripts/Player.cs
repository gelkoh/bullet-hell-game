using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction m_moveAction;

    private Camera m_mainCamera;
    private Transform m_mainCameraTransform;

    private int m_maximumHealthPoints = 100;
    private int m_remainingHealthPoints = 100;

    public static event Action<int, int> OnHealthChanged;

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
            OnHealthChanged?.Invoke(m_remainingHealthPoints, m_maximumHealthPoints);
        }
    }

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Player/Move");

        m_mainCamera = Camera.main;
        m_mainCameraTransform = m_mainCamera.GetComponent<Transform>();

        // Test the health bar
        RemainingHealthPoints = 75;
    }

    void Update()
    {
        Vector2 moveValue = m_moveAction.ReadValue<Vector2>();

        float currentX = gameObject.transform.localPosition.x;
        float currentY = gameObject.transform.localPosition.y;

        float newX = currentX += (moveValue.x * 0.01f);
        float newY = currentY += (moveValue.y * 0.01f);

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
}
