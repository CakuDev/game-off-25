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
    [Tooltip("Time in seconds to wait after a dash to be able to dash again.")]
    [SerializeField] float cooldown;
    [Tooltip("Input action to dash.")]
    [SerializeField] InputActionReference dashAction;
    [Tooltip("Input action to control the player's movement.")]
    [SerializeField] InputActionReference moveAction;

    InputAction m_dashAction;
    InputAction m_moveAction;
    Vector2 m_direction;
    bool m_canDash = true;

    void Start()
    {
        m_dashAction = dashAction.action;
        m_moveAction = moveAction.action;
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
            StartCoroutine(WaitForCooldown());
        }
    }

    IEnumerator WaitForCooldown()
    {
        m_canDash = false;
        yield return new WaitForSeconds(cooldown);
        m_canDash = true;
    }

    private void OnDrawGizmos()
    {
        Vector2 objective = ((Vector2)transform.position) + maxDistance * m_direction;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(objective, objective - maxDistance * m_direction);
        Gizmos.DrawSphere(objective, .1f);
    }
}
