using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 1f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float jumpBufferTime = 0.2f;
    public float jumpCoyoteTime = 0.2f;
    
    
    private float groundCheckRadius = 0.1f;
    private InputAction move;
    private InputAction jump;
    private Rigidbody rb;
    private bool isGrounded;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    void FixedUpdate()
    {
        Move();
        Jump();        
    }

    void Move()
    {
        var input_vector = move.ReadValue<Vector2>();
        float horizontalInput = input_vector.x;
        float verticalInput = input_vector.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = movement * moveSpeed;

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.y = rb.velocity.y;
        
        rb.velocity = transform.TransformDirection(localVelocity);
        Rotation(movement);
    }

    
    

    void Jump()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);
        if (isGrounded && jump.IsPressed())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Rotation(Vector3 movement)
    {
        if (movement.x != 0 || movement.z != 0)
        {
            Quaternion toRotate = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * 360 * Time.fixedDeltaTime);
        }
    }
}
