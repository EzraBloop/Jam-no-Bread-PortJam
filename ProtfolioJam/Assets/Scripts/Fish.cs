using System;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{

    protected Vector2 moveDir;

    [SerializeField] protected FishSO data;
    [SerializeField] protected float swimSpeed, maxSwimSpeed;
    [SerializeField] protected float waveHeight = 0.5f;
    [SerializeField] protected float waveFrequency = 2f;
    [Space(10)][SerializeField] protected GameObject fishBody;

    public Rigidbody2D rb;

    public UnityEvent<Vector2> onSwim;

    protected void Start()
    {
        InitializeFish(transform.up, this.transform.position); // TEST


    }

    protected void Update()
    {
        Swim(moveDir);
        Bob();
    }

    protected void Swim(Vector2 dir)
    {
        onSwim?.Invoke(this.transform.position);
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

    protected void Bob()
    {

        float bobOffset = Mathf.Sin(Time.time * waveFrequency) * waveHeight;

        fishBody.transform.localPosition = new Vector3(0f, bobOffset, 0f);

    }
    protected void InitializeFish(Vector2 swimDirection, Vector2 spawnLocation_)
    {
        transform.position = spawnLocation_;
        moveDir = swimDirection;
    }

}

[CreateAssetMenu(fileName = "FishData", menuName = "Fish/Fish Data")]
[Serializable]
public class FishSO : ScriptableObject
{
    public int fishID;
    public string fishName = "feesh";
    public float fishValue;
    public float fishSpeed;
    public float fishMaxSpeed;

    public GameObject prefab;


}
