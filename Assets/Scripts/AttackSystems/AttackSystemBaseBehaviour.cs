using System.Collections;
using UnityEngine;

public abstract class AttackSystemBaseBehaviour : MonoBehaviour
{
    [Tooltip("Damage that the attack is going to do to the target.")]
    [SerializeField] protected int damage;
    [Tooltip("Cooldown to wait between attacks.")]
    [SerializeField] protected float cooldown;
    [Tooltip("Layers to detect collisions. Wall is needed to destroy the projectile on collision.")]
    [SerializeField] protected LayerMask activeLayers;

    protected bool m_isOnCooldown = false;

    public abstract void Attack(Vector2 direction);

    protected IEnumerator WaitCooldown()
    {
        m_isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        m_isOnCooldown = false;
    }
}
