using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject trackingTarget;
    public float distanceFromObject;

    void Update()
    {
        gameObject.transform.position = new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y - 2, distanceFromObject);
    }
}
