using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

    public InputAction move;

    public GameObject gunPivot;

    Vector2 movementDirection;
    public event Action<Vector2> OnMove;
    Rigidbody2D rb;


    private void Awake()
    {
        move.Enable();
        move.performed += GetMoveVector;
        move.canceled += GetMoveVector;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        move.Disable();
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
}
