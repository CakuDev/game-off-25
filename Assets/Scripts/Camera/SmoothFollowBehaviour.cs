using UnityEngine;

public class SmoothFollowBehaviour : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] Transform bottomLeftLimit;
    [SerializeField] Transform topRightLimit;
    [SerializeField] float smoothFactor;

    void FixedUpdate()
    {
        Vector2 targetPosition = targetToFollow.position;
        Vector2 smoothPosition = Vector2.Lerp(targetPosition, transform.position, smoothFactor);
        Vector3 nextPosition = new(smoothPosition.x, smoothPosition.y, transform.position.z);
        if (nextPosition.x < bottomLeftLimit.position.x)
        {
            nextPosition.x = bottomLeftLimit.position.x;
        }else if (nextPosition.x > topRightLimit.position.x)
        {
            nextPosition.x = topRightLimit.position.x;
        }

        if (nextPosition.y < bottomLeftLimit.position.y)
        {
            nextPosition.y = bottomLeftLimit.position.y;
        }
        else if (nextPosition.y > topRightLimit.position.y)
        {
            nextPosition.y = topRightLimit.position.y;
        }
        transform.position = nextPosition;
    }
}
