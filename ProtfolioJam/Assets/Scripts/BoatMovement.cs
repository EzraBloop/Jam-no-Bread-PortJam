using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

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
    }
    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
    }
    private void OnDisable()
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
        OnDisable();
        plunger.GetComponent<Plunger>().OnEnable();
    }
}
