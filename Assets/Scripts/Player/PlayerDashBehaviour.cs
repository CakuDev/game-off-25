using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovementBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
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
    HealthBehaviour m_healthBehaviour;
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
        Vector2 moveValue = m_moveAction.ReadValue<Vector2>();
        if (m_moveAction.IsInProgress() && moveValue.magnitude > 0.3f)
        {
            m_direction = moveValue.normalized;
        }

        // Dash if action pressed
        if (m_canDash && m_dashAction.WasPressedThisFrame()) 
        {
            PerformDash();            
        }
    }

    void PerformDash()
    {
        // Check if there's an object in the teleport position ignoring ground obstacles
        Vector2 objective = ((Vector2) transform.position) + maxDistance * m_direction;
        int layerMask = ~LayerMask.GetMask("GroundObstacle");
        RaycastHit2D hits = Physics2D.CircleCast(objective, .1f, m_direction, .1f, layerMask);
        if (!hits)
        {
            transform.position = objective;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 objective = ((Vector2)transform.position) + maxDistance * m_direction;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(objective, objective - maxDistance * m_direction);
        Gizmos.DrawSphere(objective, .1f);
    }
}
