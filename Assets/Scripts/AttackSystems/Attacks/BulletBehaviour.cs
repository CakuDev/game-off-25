using Unity.Hierarchy;
using UnityEngine;

public class BulletBehaviour : AttackBaseBehaviour
{
    [Tooltip("Movement speed of the bullet.")]
    [SerializeField] float speed;

    void FixedUpdate()
    {
        transform.position += speed * transform.up;          
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthBehaviour healthBehaviour))
        {
            healthBehaviour.ChangeHealth(damage);
        }

        Destroy(gameObject);
    }
}
