using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    [Tooltip("Movement speed.")]
    [SerializeField] float speed = 1.0f;
    [Tooltip("Input action to control the player's movement.")]
    [SerializeField] InputActionReference moveAction;
    
    InputAction m_moveAction;
    Rigidbody2D m_rigidBody;
    bool m_canMove = true;

    void Start()
    {
        m_moveAction = moveAction.action;
        m_rigidBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (!m_canMove) return;
        Vector2 movement = m_moveAction.ReadValue<Vector2>();
        m_rigidBody.linearVelocity = speed * movement;
    }

    public void LockMovement() 
    {
        m_canMove = false;
    }

    public void UnlockMovement()
    {
        m_canMove = true;
    }
}
