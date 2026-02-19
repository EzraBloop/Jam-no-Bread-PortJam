using UnityEngine;
using UnityEngine.InputSystem;

public class Plunger : MonoBehaviour
{
    [SerializeField] InputAction moveAction, recallAction;
    [SerializeField] GameObject body, boat;
    public float fallSpeed, moveSpeed, maxSpeed, rotaionSpeed, returnSpeed;
    Rigidbody2D rb;
    public Vector2 movementDir;

    public bool acending;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        OnDisable();

        moveAction.performed += MoveInput;
        moveAction.canceled += MoveInput;
        recallAction.performed += RecallInput;
    }

    void Update()
    {
        if (!boat.GetComponent<BoatMovement>().inControl)
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
                    acending = true;
                } 
            }
            if(Vector3.Distance(transform.position, boat.transform.position) < 0.2f)
            {
                body.transform.localRotation = Quaternion.Euler(0,0,0);
                acending = false;
                rb.linearVelocity = new Vector2(0,0);
                ReturnToBoat();
            }
        }      
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(movementDir.x, 0) * moveSpeed * Time.fixedDeltaTime);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
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
        boat.GetComponent<BoatMovement>().inControl = true;
        boat.GetComponent<BoatMovement>().OnEnable();
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
