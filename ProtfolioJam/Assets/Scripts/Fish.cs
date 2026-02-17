using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{

    protected Vector2 moveDir;
    public int fishID { get; private set; }

    [SerializeField] protected FishSO data;

    [SerializeField] protected float swimSpeed = 1, maxSwimSpeed = 2;
    [SerializeField] protected float waveHeight = 0.5f;
    [SerializeField] protected float waveFrequency = 2f;
    [Space(10)][SerializeField] protected GameObject fishBody;

    public Rigidbody2D Rb;

    public UnityEvent<Vector2> Swimming;

    protected void Start()
    {
        //InitializeFish(transform.right, this.transform.position); // TEST

        if (data != null)
        {
            swimSpeed = data.fishSpeed;
            maxSwimSpeed = data.fishMaxSpeed;
            fishID = data.fishID;

        }
        else
        {
            fishID = -1;
        }


        if (moveDir == Vector2.zero) moveDir = transform.right; 
    }

    protected void Update()
    {
        Swim(moveDir);
        Bob();
    }
    #region Movement
    /// <summary>
    /// moves fish in the direction provided, rotates the fish to face the direction.
    /// </summary>
    /// <param name="dir"></param>
    protected void Swim(Vector2 dir)
    {
        
        Swimming?.Invoke(this.transform.position);
        var check = CollisionCheck();
        if (check.Count > 0)
        {
            foreach (var item in check)
            {
                if (item.rigidbody != this.GetComponent<Rigidbody2D>())
                {
                    //Debug.Log("Collision detected");
                    Pathing(check);
                    
                }
            }
        }
        Rb.AddForce(dir * swimSpeed);

        Vector2 tmpVelocity = Rb.linearVelocity;

        tmpVelocity.x = Mathf.Clamp(tmpVelocity.x, -maxSwimSpeed, maxSwimSpeed);
        tmpVelocity.y = Mathf.Clamp(tmpVelocity.y, -maxSwimSpeed, maxSwimSpeed);
        Rb.linearVelocity = tmpVelocity;

        if (Rb.linearVelocity.magnitude > 0.1) 
        {
            var angle = Mathf.Atan2(tmpVelocity.y, tmpVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }


    }



    /// <summary>
    /// local bobbing of the fish body object,
    /// </summary>
    protected void Bob()
    {

        float bobOffset = Mathf.Sin(Time.time * waveFrequency) * waveHeight;

        fishBody.transform.localPosition = new Vector3(0f, bobOffset, 0f);

    }
    protected void Pathing(List<RaycastHit2D> check)
    {
        
        moveDir *= -1;
        Rb.linearVelocity = Vector2.zero;
    }
    /// <summary>
    /// sets the spawn location, and intial swim direction;
    /// </summary>
    /// <param name="swimDirection"></param>
    /// <param name="spawnLocation_"></param>
    #endregion
    protected void InitializeFish(Vector2 swimDirection, Vector2 spawnLocation_)
    {
        transform.position = spawnLocation_;
        moveDir = swimDirection;
    }

    #region Collisions
    protected List<RaycastHit2D> CollisionCheck()
    {
        float rayDistance = 2f;

        var hits = Physics2D.RaycastAll(transform.position, moveDir, rayDistance);
        return hits.Where(h => h.collider.gameObject != fishBody).ToList();
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    protected void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    #endregion
    #region Gizmos
    protected void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir * 3f); // swim direction

        if (Rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Rb.linearVelocity);
        }
    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2f);
    }
    #endregion


}
[CreateAssetMenu(fileName = "FishData", menuName = "Fish/Fish Data")]
[Serializable]
public class FishSO : ScriptableObject
{
    public int fishID = 0;
    public string fishName = "feesh";
    public float fishValue = 0;
    public float fishSpeed = 1;
    public float fishMaxSpeed = 1;

    public GameObject prefab;


}
