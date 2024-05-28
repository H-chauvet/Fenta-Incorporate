using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;
    public float jumpForce = 1f;
    public LayerMask groundLayer;
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
        Vector2 input_vector = move.ReadValue<Vector2>();
        float horizontalInput = input_vector.x;
        float verticalInput = input_vector.y;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = RotateVector3ToCameraSpace(movement);
        Vector3 velocity = movement * moveSpeed;

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.y = rb.velocity.y;
        isWalking = localVelocity != Vector3.zero ? true : false;
        rb.velocity = transform.TransformDirection(localVelocity);
        Rotation(movement);
    }
    
    void Jump()
    {
        Vector3 currentScale = transform.localScale;
        float normalizedGroundCheckRadius = groundCheckRadius * currentScale.y;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, normalizedGroundCheckRadius, groundLayer);
        isJumping = isGrounded ? false : true;
        if (isGrounded && jump.IsPressed())
        {
            float normalizedJumpForce = jumpForce * 20 * currentScale.y;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * normalizedJumpForce, ForceMode.Impulse);
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

    private Vector3 RotateVector3ToCameraSpace(Vector3 vector)
    {
        _mainCameraForward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;
        _mainCameraForward.y = 0.0f;
        right.y = 0.0f;
        _mainCameraForward.Normalize();
        right.Normalize();
        return vector.x * right + vector.z * _mainCameraForward;
    }
    
}