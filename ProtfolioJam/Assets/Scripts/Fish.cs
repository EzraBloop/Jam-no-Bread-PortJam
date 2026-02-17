using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{

    protected Vector2 moveDir;
    public int FishID { get; private set; }

    [SerializeField] protected FishSO data;
    [Serializable]public struct BobSettings
    {
        public float waveHeight;
        public float waveFrequency;
    }
    [SerializeField]protected BobSettings bobSettings = new BobSettings { waveHeight = 0.5f, waveFrequency = 2f };

    [SerializeField] protected float swimAcceleration = 1, maxSwimVelocity = 2; //overwritten by data if present
    [Space(10)][SerializeField] protected GameObject fishBody;

    private Rigidbody2D rb;

    public UnityEvent<Vector2> Swimming;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //InitializeFish(transform.right, this.transform.position); // TEST

        if (data != null)
        {
            swimAcceleration = data.fishSpeed;
            maxSwimVelocity = data.fishMaxVelocity;
            FishID = data.fishID;

        }
        else
        {
            FishID = -1;
        }


        if (moveDir == Vector2.zero) moveDir = transform.right; 
    }

    public void Update()
    {
        Swim(moveDir);
        Bob();
    }
    public void InitializeFish(Vector2 swimDirection, Vector2 spawnLocation)
    {
        transform.position = spawnLocation;
        moveDir = swimDirection;
    }
    #region Movement
    /// <summary>
    /// moves fish in the direction provided, rotates the fish to face the direction.
    /// </summary>
    /// <param name="dir"></param>
    protected void Swim(Vector2 dir)
    {
        
        Swimming?.Invoke(this.transform.position);
        var check = ForwardCollisionCheck();
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
        rb.AddForce(dir * swimAcceleration);

        Vector2 tmpVelocity = rb.linearVelocity;

        tmpVelocity.x = Mathf.Clamp(tmpVelocity.x, -maxSwimVelocity, maxSwimVelocity);
        tmpVelocity.y = Mathf.Clamp(tmpVelocity.y, -maxSwimVelocity, maxSwimVelocity);
        rb.linearVelocity = tmpVelocity;

        if (rb.linearVelocity.magnitude > 0.1) 
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

        float bobOffset = Mathf.Sin(Time.time * bobSettings.waveFrequency) * bobSettings.waveHeight;

        fishBody.transform.localPosition = new Vector3(0f, bobOffset, 0f);

    }
    protected void Pathing(List<RaycastHit2D> check)
    {
        
        moveDir *= -1;
        rb.linearVelocity = Vector2.zero;
    }
    /// <summary>
    /// sets the spawn location, and intial swim direction;
    /// </summary>
    /// <param name="swimDirection"></param>
    /// <param name="spawnLocation_"></param>
    #endregion

    #region Collisions
    protected List<RaycastHit2D> ForwardCollisionCheck()
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

        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rb.linearVelocity);
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
    [Header("Fish Stats")]
    [Space(10)]
    public string fishName = "feesh";
    public float fishValue = 0;
    public int fishCaught = 0;
    public int fishSold = 0;
    [Space(10)]
    public float fishSpeed = 1;
    public float fishMaxVelocity = 1;

    public GameObject prefab;


}
