using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractiveBaseBehaviour : MonoBehaviour
{
    [Tooltip("Can the player interact with this object?")]
    public bool isEnabled = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnabled && collision.CompareTag(Tags.PLAYER))
        {
            OnPlayerEntered();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (isEnabled && collision.CompareTag(Tags.PLAYER))
        {
            OnPlayerExited();
        }
    }

    void OnPlayerEntered()
    {
        //TODO: Add common functionality
    }

    void OnPlayerExited()
    {
        //TODO: Add common functionality
    }

    public abstract void Interact();
}
