using System.Collections;
using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.PLAYER))
        {
            StartCoroutine(FallDown(collision.gameObject));
        }
    }

    IEnumerator FallDown(GameObject player)
    {
        // Disable movement, dash and physics collision movement
        player.GetComponent<PlayerMovementBehaviour>().LockMovement();
        player.GetComponent<PlayerDashBehaviour>().LockDash();
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(1);

        // Enable movement
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.GetComponent<PlayerMovementBehaviour>().UnlockMovement();
        player.GetComponent<PlayerDashBehaviour>().UnlockDash();

        // Reposition and deal damage
        player.GetComponent<PlayerDashBehaviour>().Reposition();
        player.GetComponent<HealthBehaviour>().ChangeHealth(-1);
    }
}
