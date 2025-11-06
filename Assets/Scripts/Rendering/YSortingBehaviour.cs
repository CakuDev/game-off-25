using UnityEngine;

public class YSortingBehaviour : MonoBehaviour
{
    [Tooltip("Set the Z position only on Start?")]
    [SerializeField] bool isStatic = true;

    void Start()
    {
        SetZ();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStatic)
        {
            SetZ();
        }
    }

    void SetZ()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = currentPosition.y;
        transform.position = currentPosition;
    }
}
