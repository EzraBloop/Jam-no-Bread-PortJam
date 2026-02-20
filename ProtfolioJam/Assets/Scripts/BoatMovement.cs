using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{

    public Camera cam;

    public float moveSpeed;
    public float rotSpeed;
    public float fireForce;

    public bool inControl;

    public InputAction move;
    public InputAction fire;
    public GameObject gunPivot;

    public GameObject plunger;
    public GameObject barrel;

    Vector2 movementDirection;
    public event Action<Vector2> OnMove;
    Rigidbody2D rb;

    private Earning ear;
    private void Awake()
    {
        move.performed += GetMoveVector;
        move.canceled += GetMoveVector;

        fire.performed += FirePlunger;

        rb = GetComponent<Rigidbody2D>();
        inControl = true;
    }
    public void OnEnable()
    {
        move.Enable();
        fire.Enable();
    }
    public void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
    private void Update()
    {
        transform.position += new Vector3(movementDirection.x, 0, 0) * moveSpeed * Time.deltaTime;
        gunPivot.transform.localEulerAngles += new Vector3(0, 0, -movementDirection.y) * rotSpeed * Time.deltaTime;

        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            if(cam.depth == 0)
            {
                cam.depth = -2;
                ear.DisplayFish();
            }
            else
            {
                cam.depth = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        movementDirection.x = 0;
    }

    public void GetMoveVector(InputAction.CallbackContext c)
    {
        movementDirection = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementDirection);
    }

    public void FirePlunger(InputAction.CallbackContext c)
    {
        plunger.GetComponent<Rigidbody2D>().AddForce(barrel.transform.TransformDirection(Vector3.down) * fireForce * Time.deltaTime, ForceMode2D.Impulse);
        inControl = false;
        plunger.GetComponent<Plunger>().OnEnable();
        plunger.GetComponent<Plunger>().Reappear();
        OnDisable();
    }
}
