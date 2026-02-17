using UnityEngine;

public class Fish : MonoBehaviour
{
    
    private Vector2 moveDir;

    [SerializeField] private float swimSpeed, maxSwimSpeed;
    [SerializeField] private float waveHeight = 0.5f;
    [SerializeField] private float waveFrequency = 2f;
    [SerializeField] private GameObject fishBody;

    Vector3 spawnLocation;

    public Collider2D fishCollider;
    public Rigidbody2D rb;



    private void Start()
    {
        spawnLocation = transform.position;
        InitializeFish(transform.right, this.transform.position);


    }

    private void Update()
    {
        Swim(moveDir);
        Bob();
    }

    private void Swim(Vector2 dir)
    {
        rb.AddForce(dir * swimSpeed);

        Vector2 tmpVelocity = rb.linearVelocity;

        tmpVelocity.x = Mathf.Clamp(tmpVelocity.x, -maxSwimSpeed, maxSwimSpeed);
        tmpVelocity.y = Mathf.Clamp(tmpVelocity.y, -maxSwimSpeed, maxSwimSpeed);
        rb.linearVelocity = tmpVelocity;

        if (rb.linearVelocity.magnitude > 0.1) 
        {
            var angle = Mathf.Atan2(tmpVelocity.y, tmpVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }


    }

    private void Bob()
    {

        float bobOffset = Mathf.Sin(Time.time * waveFrequency) * waveHeight;

        fishBody.transform.localPosition = new Vector3(0f, bobOffset, 0f);

    }
    public void InitializeFish(Vector2 swimDirection, Vector2 spawnLocation_)
    {
        transform.position = spawnLocation_;
        spawnLocation = spawnLocation_;
        moveDir = swimDirection;
    }

}

[CreateAssetMenu(fileName = "FishData", menuName = "Fish/Fish Data")]
public class FishSO : ScriptableObject
{
    public int fishID;
    public string fishName;
    public float fishValue;
    public float fishSpeed;
    public float fishMaxSpeed;

    public GameObject prefab;


}
