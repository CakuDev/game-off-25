using UnityEngine;

public class SmoothFollowBehaviour : MonoBehaviour
{
    [Tooltip("Smoothness factor of the camera movement [0.001f, 1].")]
    [Range(0.001f, 1)]
    [SerializeField] float smoothFactor;
    [Tooltip("Target to follow with the camera within the limits.")]
    [SerializeField] Transform targetToFollow;
    [Header("Position Limits")]
    [Tooltip("Bottom left limit for the camera position.")]
    [SerializeField] Transform bottomLeftLimit;
    [Tooltip("Top right limit for the camera position.")]
    [SerializeField] Transform topRightLimit;

    void FixedUpdate()
    {
        // Calculate next camera position
        Vector2 targetPosition = targetToFollow.position;
        Vector2 smoothPosition = Vector2.Lerp(targetPosition, transform.position, smoothFactor);
        Vector3 nextPosition = new(smoothPosition.x, smoothPosition.y, transform.position.z);
        
        // Limit x axis position
        if (nextPosition.x < bottomLeftLimit.position.x)
        {
            nextPosition.x = bottomLeftLimit.position.x;
        }else if (nextPosition.x > topRightLimit.position.x)
        {
            nextPosition.x = topRightLimit.position.x;
        }

        // Limit y axis position
        if (nextPosition.y < bottomLeftLimit.position.y)
        {
            nextPosition.y = bottomLeftLimit.position.y;
        }
        else if (nextPosition.y > topRightLimit.position.y)
        {
            nextPosition.y = topRightLimit.position.y;
        }

        // Set position
        transform.position = nextPosition;
    }
}
