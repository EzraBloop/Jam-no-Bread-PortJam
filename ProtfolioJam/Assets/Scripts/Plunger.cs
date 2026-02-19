using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plunger : MonoBehaviour
{
    [SerializeField] InputAction moveAction, recallAction;
    [SerializeField] GameObject body, boat, flange, captured;
    public float fallSpeed, moveSpeed, maxSpeed, rotaionSpeed, returnSpeed;
    Rigidbody2D rb;
    public Vector2 movementDir;

    public bool acending;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        transform.position = boat.transform.position;
        OnDisable();

        moveAction.performed += MoveInput;
        moveAction.canceled += MoveInput;
        recallAction.performed += RecallInput;
    }

    void Update()
    {
        if (!boat.GetComponentInParent<BoatMovement>().inControl)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
            if (acending)
            {
                transform.position = Vector3.MoveTowards(transform.position, boat.transform.position, returnSpeed * Time.deltaTime);
                
            }
            else
            {
            if(hit == false)
                {
                    transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    captured = hit.collider.gameObject.transform.parent.gameObject;
                    acending = true;
                } 
            }
            if(Vector3.Distance(transform.position, boat.transform.position) < 0.01f)
            {
                Destroy(captured.gameObject);
                body.transform.localRotation = Quaternion.Euler(0,0,0);
                acending = false;
                rb.linearVelocity = new Vector2(0,0);
                ReturnToBoat();
            }
            if(captured != null)
            {
                captured.transform.position = flange.transform.position;
            } 
        }      
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(movementDir.x, 0) * moveSpeed * Time.fixedDeltaTime);
        if(transform.position.y <= 0)
        { 
          rb.linearDamping = 1;  
        }
        else
        {
            rb.linearDamping = 0;
        }
        if(movementDir.x > 0.5)
        {
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(0,0, 45), rotaionSpeed * Time.fixedDeltaTime);
        }
        if(movementDir.x < -0.5)
        {
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(0,0, -45), rotaionSpeed * Time.fixedDeltaTime);
        }
    }

    public void ReturnToBoat()
    {
        boat.GetComponentInParent<BoatMovement>().inControl = true;
        boat.GetComponentInParent<BoatMovement>().OnEnable();
        OnDisable();
    }

    public void MoveInput(InputAction.CallbackContext c)
    {
        movementDir =  c.ReadValue<Vector2>();
    }
    public void RecallInput(InputAction.CallbackContext c)
    {
        acending = true;
    }

    public void OnEnable()
    {
        moveAction.Enable();
        recallAction.Enable();
    }
    public void OnDisable()
    {
        moveAction.Disable();
        recallAction.Disable();
    }
}
