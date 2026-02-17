using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject trackingTarget;

    void Update()
    {
        gameObject.transform.position = new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y - 2, -10);
    }
}
