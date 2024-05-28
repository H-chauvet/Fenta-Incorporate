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
    // public Transform groundCheck;
    public float jumpBufferTime = 0.2f;
    public float jumpCoyoteTime = 0.2f;

    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isWalking;
    
    private float groundCheckRadius = 0.1f;
    private InputAction move;
    private InputAction jump;
    private Rigidbody rb;
    private bool isGrounded;

    private Transform _mainCameraTransform;
    private Vector3 _mainCameraForward;



    private void Start()
    {
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    void FixedUpdate()
    {
        var input_vector = move.ReadValue<Vector2>();
        isWalking = input_vector != Vector2.zero;
        if (isWalking)
            Move(input_vector);
        Jump();        
    }

    void Move(Vector2 input_vector)
    {
        float horizontalInput = input_vector.x;
        float verticalInput = input_vector.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        ProjectVectorToCameraCoordinateSpace(ref movement);
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
            isJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce * 50, ForceMode.Impulse);
        }
        else
        {
            isJumping = false;
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
    
    private void ProjectVectorToCameraCoordinateSpace(ref Vector3 vectorToProject)
    {
        _mainCameraForward = _mainCameraTransform.forward;
        var right = _mainCameraTransform.right;
        _mainCameraForward.y = 0.0f;
        right.y = 0.0f;
        _mainCameraForward.Normalize();
        right.Normalize();
        vectorToProject = vectorToProject.x * right + vectorToProject.z * _mainCameraForward;
    }
}
