using UnityEditor.UI;
using UnityEngine;

public class RangeAttackSystemBehaviour : AttackSystemBaseBehaviour
{
    [Tooltip("Prefab of the projectile to attack with.")]
    [SerializeField] GameObject projectilePrefab;

    public override void Attack(Vector2 direction)
    {
        // Cooldown management
        if (m_isOnCooldown) return;
        StartCoroutine(WaitCooldown());

        GameObject projectile = Instantiate(projectilePrefab);
        
        // Set rotation from direction of the attack
        float rotation = Vector2.SignedAngle(transform.up, direction);
        projectile.transform.Rotate(new Vector3(0f, 0f, rotation));
        
        // Set position with a little offset to avoid first frame collisions
        projectile.transform.position = transform.position + (Vector3)direction;
        
        // Set layerMask to hit only the configurated layers
        projectile.GetComponent<Collider2D>().callbackLayers = activeLayers;

        // Set damage
        projectile.GetComponent<AttackBaseBehaviour>().damage = damage;
    }
}
