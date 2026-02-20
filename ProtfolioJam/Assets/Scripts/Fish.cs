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

    [SerializeField] protected float swimAcceleration = 1, maxSwimVelocity = 2; //overwritten by data if present
    [Space(10)][SerializeField] protected GameObject fishBody;

    private Rigidbody2D rb;
    public bool IsCaught;
    public UnityEvent<Vector2> Swimming;

    public void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        //InitializeFish(transform.right, this.transform.position); // TEST
        rb.gravityScale = 0f;

        if (data != null)
        {
            swimAcceleration = data.fishSpeed;
            maxSwimVelocity = data.fishMaxVelocity;
            FishID = data.fishID;
            if (data.direction == FishSO.InitialDirection.LEFT)
            {
                moveDir = -transform.right;
            }
            else
            {
                moveDir = transform.right;
            }

        }
        else
        {
            FishID = -1;
            Debug.LogWarning($"{gameObject.name}'s Scriptable Object is missing");
        }


        if (moveDir == Vector2.zero) moveDir = transform.right;
        if (this.data.direction == FishSO.InitialDirection.LEFT)
        {
            fishBody.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Update()
    {
        if (!IsCaught)
        {
            Swim(moveDir);
        }
        else
        {
            fishBody.transform.localPosition = Vector3.zero; // reset Bob transform changes;
        }
    }
    public void CatchFish(int amountCaught)
    {
        IsCaught = true;
        FishInventory.Instance.EditFishCount(this.data, 1);
        Destroy(this.gameObject);

    }
    /// <summary>
    /// sets the spawn location, and intial swim direction;
    /// </summary>
    /// <param name="swimDirection"></param>
    /// <param name="spawnLocation_"></param>
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
            var sprite = fishBody.GetComponent<SpriteRenderer>();

            if (rb.linearVelocity.x <= 0)
            {
                sprite.flipY = true;

            }
            else
            {
                sprite.flipY = false;
            }
        }



        var angle = Mathf.Atan2(tmpVelocity.y, tmpVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }



    protected void Pathing(List<RaycastHit2D> check)
    {

        //rb.linearVelocity = Vector2.zero;

        moveDir *= -1;
    }
    #endregion

    #region Collisions
    protected List<RaycastHit2D> ForwardCollisionCheck()
    {
        float rayDistance = 2f;

        var hits = Physics2D.RaycastAll(transform.position, moveDir, rayDistance);
        return hits.Where(h => h.collider.gameObject != fishBody || h.collider.GetComponentInParent<Fish>() == null).ToList();
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

