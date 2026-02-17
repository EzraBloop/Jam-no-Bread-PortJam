using UnityEngine;

public class Fish : MonoBehaviour
{
    private Vector2 _moveDir;
    [SerializeField] private float _swimSpeed, _maxSwimSpeed;

    [SerializeField] private float waveHeight = 0.5f;
    [SerializeField] private float waveFrequency = 2f;

    Vector3 _spawnLocation;

    public Collider2D _collider;
    public Rigidbody2D _rb;

    

    private void Start()
    {
        _moveDir = this.transform.right;
        _swimSpeed = 1;

        _spawnLocation = transform.position;

    }
    private void Update()
    {
        Swim(transform.right);
    }

    private void Swim(Vector2 dir_)
    {
        _rb.AddForce(dir_);



        Vector2 tmp = _rb.linearVelocity;

        tmp.x = Mathf.Clamp(tmp.x, -_maxSwimSpeed, _maxSwimSpeed);
        _rb.linearVelocity = tmp;


    }
    private void Bob()
    {
        float newY = _spawnLocation.y + Mathf.Sin(Time.time * waveFrequency) * waveHeight;
        Vector3 pos = transform.position;

    }
}
