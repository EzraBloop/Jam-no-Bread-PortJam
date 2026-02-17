using UnityEngine;

public class Fish : MonoBehaviour
{
    private Vector2 _moveDir;
    [SerializeField] private float swimSpeed, maxSwimSpeed;

    [SerializeField] private float waveHeight = 0.5f;
    [SerializeField] private float waveFrequency = 2f;

    Vector3 _spawnLocation;

    public Collider2D fishCollider;
    public Rigidbody2D rb;

    

    private void Start()
    {
        _moveDir = this.transform.right;
        swimSpeed = 1;

        _spawnLocation = transform.position;

    }
    private void Update()
    {
        Swim(transform.right);
        Bob();
    }

    private void Swim(Vector2 dir_)
    {
        rb.AddForce(dir_);



        Vector2 tmp = rb.linearVelocity;

        tmp.x = Mathf.Clamp(tmp.x, -maxSwimSpeed, maxSwimSpeed);
        rb.linearVelocity = tmp;


    }
    private void Bob()
    {
        float newY = _spawnLocation.y + Mathf.Sin(Time.time * waveFrequency) * waveHeight;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;

    }
}
