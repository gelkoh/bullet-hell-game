using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction m_moveAction;

    private Camera m_mainCamera;
    private Transform m_mainCameraTransform;

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Player/Move");

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

        Vector3 playerScreenPosition = m_mainCamera.WorldToScreenPoint(gameObject.transform.localPosition);
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        float diffX = mouseScreenPosition.x - playerScreenPosition.x;
        float diffY = mouseScreenPosition.y - playerScreenPosition.y;

        float angle = -Mathf.Atan2(diffX, diffY) * Mathf.Rad2Deg + 90f;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
