using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

    public bool inControl;

    public InputAction move;
    public InputAction fire;
    public GameObject gunPivot;

    public GameObject plunger;

    Vector2 movementDirection;
    public event Action<Vector2> OnMove;
    Rigidbody2D rb;


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
    }

    public void GetMoveVector(InputAction.CallbackContext c)
    {
        movementDirection = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementDirection);
    }

    public void FirePlunger(InputAction.CallbackContext c)
    {
        inControl = false;
        OnDisable();
        plunger.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1000 * Time.deltaTime, ForceMode2D.Impulse);
        plunger.GetComponent<Plunger>().OnEnable();
    }
}
