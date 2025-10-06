using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction m_moveAction;
    private InputAction m_lookAction;

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Player/Move");
        m_lookAction = InputSystem.actions.FindAction("Player/Look");
    }

    void Update()
    {
        Vector2 moveValue = m_moveAction.ReadValue<Vector2>();

        float currentX = gameObject.transform.localPosition.x;
        float currentY = gameObject.transform.localPosition.y;

        float newX = currentX += (moveValue.x * 0.01f);
        float newY = currentY += (moveValue.y * 0.01f);

        gameObject.transform.localPosition = new Vector3(newX, newY);
    }
}
