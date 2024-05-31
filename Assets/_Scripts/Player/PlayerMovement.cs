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
    public float jumpBufferTime = 0.1f;
    public float jumpCoyoteTime = 0.1f;

    private bool canJump = true;
    // private float currentJumpPressedTime = 0f;
    // private float maxJumpPressedTime = 0.2f;
    private float currentFallingTime = 0f;
    private float currentJumpBufferTime = 0f;
    public float gravity = 9.8f;

    public List<GameObject> gameObjectsScaleNormalization = new List<GameObject>();
    private float normalizedScale = 1f;



    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isFalling;
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
        AttachComponents();
        NormalizeScale();
    }
    
    void AttachComponents()
    {
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    void NormalizeScale()
    {
        foreach (GameObject go in gameObjectsScaleNormalization)
        {
            normalizedScale *= go.transform.localScale.y;
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Get input from player
        Vector2 input_vector = move.ReadValue<Vector2>();
        float horizontalInput = input_vector.x;
        float verticalInput = input_vector.y;

        // Getting movement and calculating velocity from input
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = RotateVector3ToCameraSpace(movement);
        Vector3 velocity = movement * moveSpeed * normalizedScale * 10;

        // Apply velocity to the player
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.y = rb.velocity.y;
        isWalking = localVelocity != Vector3.zero ? true : false;
        rb.velocity = transform.TransformDirection(localVelocity);

        // Rotate player
        Rotation(movement);
    }

    void Rotation(Vector3 movement)
    {
        // Rotate player to the direction of movement
        if (movement.x != 0 || movement.z != 0)
        {
            Quaternion toRotate = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * 360 * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        float normalizedGroundCheckRadius = groundCheckRadius * normalizedScale; 
        // Draw the sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, normalizedGroundCheckRadius);
    }
    
    void Jump()
    {
        float normalizedGroundCheckRadius = groundCheckRadius * normalizedScale;
            
        // Check if player is on ground
        isGrounded = Physics.SphereCast(transform.position, 0f, Vector3.down, out RaycastHit hit, normalizedGroundCheckRadius, groundLayer);
        Debug.Log(isGrounded);
        
        // Jump if one condition is met
        if ((canJump && jump.IsPressed()) || (canJump && currentJumpBufferTime > 0))
        {
            isJumping = true;
            canJump = false;
            currentJumpBufferTime = 0.0f;
            float normalizedJumpForce = jumpForce * normalizedScale * 10;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * normalizedJumpForce, ForceMode.Impulse);
        }
        // Reset jump conditions
        if (isGrounded)
        {
            isJumping = false;
            canJump = true;
            isFalling = false;
            currentFallingTime = 0;
        }
        // Falling function if the player is not on the ground
        if (!isGrounded) {
            Fall();
        }
        
    }

    void Fall()
    {
        isFalling = true;
        // Coyote time
        currentFallingTime += Time.fixedDeltaTime;
        if (canJump && currentFallingTime > jumpCoyoteTime)
        {
            canJump = false;
        }
        
        // Jump buffer time
        currentJumpBufferTime -= Time.fixedDeltaTime;
        if (!canJump && jump.IsPressed()) {
            currentJumpBufferTime = jumpBufferTime;
        }

        // Apply gravity
        float normalizedGravity = gravity * normalizedScale * 10;
        rb.AddForce(Vector3.down * normalizedGravity, ForceMode.Acceleration);
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