using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plunger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] InputAction moveAction, recallAction, boostAction;
    [SerializeField] GameObject body, boat, flange;
    public List<GameObject> captures = new List<GameObject>();
    public float fallSpeed, moveSpeed, maxSpeed, rotaionSpeed, returnSpeed;
    public int fishCaptureable;
    Rigidbody2D rb;
    public Vector2 movementDir;

    public bool acending, hasBoost;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        transform.position = boat.transform.position;
        gameManager = GameManager.Instance;
        CheckUpgrades();
        OnDisable();
        

        moveAction.performed += MoveInput;
        moveAction.canceled += MoveInput;
        recallAction.performed += RecallInput;
        boostAction.performed += BoostInput;
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
                    if(hit.collider.gameObject.tag == "Fish")
                    {
                        hit.collider.enabled = false;
                        captures.Add(hit.collider.gameObject.transform.parent.gameObject);
                    }
                    if(captures.Count >= fishCaptureable)
                    {
                        acending = true;
                    }
                    
                } 
            }
            if(Vector3.Distance(transform.position, boat.transform.position) < 0.01f)
            {
                foreach(GameObject c in captures)
                {
                    Destroy(c.gameObject);
                }
                body.transform.localRotation = Quaternion.Euler(0,0,0);
                acending = false;
                rb.linearVelocity = new Vector2(0,0);
                captures.Clear();
                ReturnToBoat();
            }
            if(captures != null)
            {
                foreach(GameObject c in captures)
                {
                    c.transform.position = flange.transform.position;
                }
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
    public void CheckUpgrades()
    {
        moveSpeed = gameManager.turnSpeed;
        hasBoost = gameManager.fallBoostAvailible;
        fishCaptureable = gameManager.fishCaptureable;
        boat.GetComponentInParent<BoatMovement>().fireForce = gameManager.initialLaunchForce;
    }

    public void MoveInput(InputAction.CallbackContext c)
    {
        movementDir =  c.ReadValue<Vector2>();
    }
    public void RecallInput(InputAction.CallbackContext c)
    {
        acending = true;
    }
    public void BoostInput(InputAction.CallbackContext c)
    {
        //WE BOOSTING
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
