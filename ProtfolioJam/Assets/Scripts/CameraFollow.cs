using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject trackingTarget;
    public float distanceFromObject;

    void Update()
    {
        gameObject.transform.position = new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y - 2, distanceFromObject);
        if(transform.position.x <= -2)
        {
            transform.position = new Vector3(-2, transform.position.y, distanceFromObject);
        }
        if(transform.position.x >= 2)
        {
            transform.position = new Vector3(2, transform.position.y, distanceFromObject);
        }
        if(transform.position.y <= -148)
        {
            transform.position = new Vector3(transform.position.x, -148, distanceFromObject);
        }
    }
}
