using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractiveBaseBehaviour : MonoBehaviour
{
    [Tooltip("Can the player interact with this object?")]
    public bool isEnabled = true;
    [Tooltip("Visual indicator to highlight this object when player is close.")]
    [SerializeField] private SpriteRenderer interactionIndicator;

    private void Start()
    {
        interactionIndicator.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnabled && collision.TryGetComponent(out InteractionBehaviour interactionBehaviour))
        {
            OnPlayerEntered(interactionBehaviour);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (isEnabled && collision.TryGetComponent(out InteractionBehaviour interactionBehaviour))
        {
            OnPlayerExited(interactionBehaviour);
        }
    }

    void OnPlayerEntered(InteractionBehaviour interactionBehaviour)
    {
        interactionBehaviour.AddInteractiveObject(this);
    }

    void OnPlayerExited(InteractionBehaviour interactionBehaviour)
    {
        interactionBehaviour.RemoveInteractiveObject(this);
    }

    public void EnableIndicator()
    {
        interactionIndicator.gameObject.SetActive(true);
    }

    public void DisableIndicator()
    {
        interactionIndicator.gameObject.SetActive(false);
    }

    public abstract void Interact();
}
