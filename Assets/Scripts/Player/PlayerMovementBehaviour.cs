using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] InputActionReference moveAction;
    
    InputAction m_moveAction;
    Rigidbody2D m_rigidBody;
    void Start()
    {
        m_moveAction = moveAction.action;
        m_rigidBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector2 movement = m_moveAction.ReadValue<Vector2>();
        m_rigidBody.linearVelocity = speed * movement;

    }
}
