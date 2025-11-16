using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerDashBehaviour : MonoBehaviour
{
    [Tooltip("Maximum distance to dash.")]
    [SerializeField] float maxDistance;
    [Tooltip("Dash speed.")]
    [SerializeField] float speed;
    [Tooltip("Time in seconds to wait after a dash to be able to dash again.")]
    [SerializeField] float cooldown;
    [Tooltip("Time in seconds for the player to avoid damage.")]
    [SerializeField] float invencibilityTime;
    [Tooltip("Layers ignored by the player while dashing.")]
    [SerializeField] LayerMask layersToDisable;
    [Tooltip("Input action to dash.")]
    [SerializeField] InputActionReference dashAction;
    [Tooltip("Input action to control the player's movement.")]
    [SerializeField] InputActionReference moveAction;

    InputAction m_dashAction;
    InputAction m_moveAction;
    Rigidbody2D m_rigidbody;
    PlayerMovementBehaviour m_playerMovementBehaviour;
    Vector2 m_direction;
    bool m_canDash = true;

    void Start()
    {
        m_dashAction = dashAction.action;
        m_moveAction = moveAction.action;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    void Update()
    {
        // Read movement to save last direction
        if (m_moveAction.IsInProgress())
        {
            m_direction = m_moveAction.ReadValue<Vector2>();
        }

        // Dash if action pressed
        if (m_canDash && m_dashAction.WasPressedThisFrame()) 
        {
            StartCoroutine(PerformDash());               
        }
    }

    IEnumerator PerformDash()
    {
        // Unlock movement and set dash velocity
        m_canDash = false;
        m_playerMovementBehaviour.LockMovement();
        m_rigidbody.excludeLayers += layersToDisable;
        m_rigidbody.linearVelocity = speed * m_direction;

        yield return new WaitForSeconds(maxDistance/speed);

        // Finish dash and unlock movement
        m_rigidbody.excludeLayers -= layersToDisable;
        m_playerMovementBehaviour.UnlockMovement();


        yield return new WaitForSeconds(cooldown);
        
        // Enable dash after cooldown
        m_canDash = true;
    }
}
