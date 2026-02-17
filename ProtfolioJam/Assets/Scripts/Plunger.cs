using UnityEngine;
using UnityEngine.InputSystem;

public class Plunger : MonoBehaviour
{
    [SerializeField] InputAction moveAction;
    [SerializeField] GameObject body, boat;
    public float fallSpeed, moveSpeed, maxSpeed, rotaionSpeed, returnSpeed;
    Rigidbody2D rb;
    public Vector2 movementDir;

    bool acending;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        OnEnable();

        moveAction.performed += MoveInput;
        moveAction.canceled += MoveInput;
    }

    void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        if(hit == false)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log(hit.collider.gameObject.tag);
            acending = true;
        }
        if (acending)
        {
            transform.position = Vector3.MoveTowards(transform.position, boat.transform.position, returnSpeed * Time.deltaTime);
        }         
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(movementDir.x, 0) * moveSpeed * Time.fixedDeltaTime);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        if(movementDir.x == 1)
        {
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(0,0, 45), rotaionSpeed * Time.fixedDeltaTime);
        }
        if(movementDir.x == -1)
        {
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(0,0, -45), rotaionSpeed * Time.fixedDeltaTime);
        }
    }

    public void ReturnToBoat()
    {
        boat.GetComponent<BoatMovement>().enabled = true;
        gameObject.GetComponent<Plunger>().enabled = false;
    }

    public void MoveInput(InputAction.CallbackContext c)
    {
        movementDir =  c.ReadValue<Vector2>();
    }

    void OnEnable()
    {
        moveAction.Enable();
    }
}
