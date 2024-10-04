using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;

    [SerializeField] private Rigidbody rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Movement();

    }

    private void Movement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(move));
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}