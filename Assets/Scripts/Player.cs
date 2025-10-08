using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction m_moveAction;
    private InputAction m_lookAction;

    private Camera m_mainCamera;
    private Transform m_mainCameraTransform;

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Player/Move");
        m_lookAction = InputSystem.actions.FindAction("Player/Look");

        m_mainCamera = Camera.main;
        m_mainCameraTransform = m_mainCamera.GetComponent<Transform>();
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
    }
}
