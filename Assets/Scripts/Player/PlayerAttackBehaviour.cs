using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackBehaviour : MonoBehaviour
{
    [Tooltip("Input action of the player's attack.")]
    [SerializeField] InputActionReference attackAction;
    [Tooltip("Input action of the relative direction the player is looking at. Gamepad joystick variation.")]
    [SerializeField] InputActionReference lookRelativeAction;
    [Tooltip("Input action of the absolute direction the player is looking at. Mouse Position.")]
    [SerializeField] InputActionReference lookAbsoluteAction;
    [Tooltip("Different attacks of the player.")]
    [SerializeField] List<AttackSystemBaseBehaviour> attacks;

    AttackSystemBaseBehaviour m_activeAttackSystem;
    InputAction m_attackAction;
    InputAction m_lookRelativeAction;
    InputAction m_lookAbsoluteAction;

    void Start()
    {
        m_attackAction = attackAction.action;
        m_lookRelativeAction = lookRelativeAction.action;
        m_lookAbsoluteAction = lookAbsoluteAction.action;
        if (attacks.Count > 0)
        {
            m_activeAttackSystem = attacks[0];
        }
    }

    void Update()
    {
        if (m_attackAction.IsPressed())
        {
            m_activeAttackSystem.Attack(GetDirection());
        }
    }

    Vector2 GetDirection()
    {
        // If relativeDirection is being used, take its value
        Vector2 relativeDirection = m_lookRelativeAction.ReadValue<Vector2>();
        if (relativeDirection.magnitude > 0f) return relativeDirection;

        // If not, calculate relative direction with mouse position
        Vector2 absoluteDirection = Camera.main.ScreenToWorldPoint(m_lookAbsoluteAction.ReadValue<Vector2>());
        Vector2 currentPosition = transform.position;
        return (absoluteDirection - currentPosition).normalized;
    }
}
