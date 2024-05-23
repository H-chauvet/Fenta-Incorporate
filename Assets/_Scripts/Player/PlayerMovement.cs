using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float rotationSpeed = 1f;

    public float jumpForce = 10f;
    public float jumpSpeed = 5f;
    public float gravity = 9.8f;
    private float jumpVelocity = 0f;

    private InputAction move;
    private InputAction jump;



    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var input_vector = move.ReadValue<Vector2>();

        float horizontalInput = input_vector.x;
        float verticalInput = input_vector.y;

        bool isGrounded = IsGrounded();

        if (jump.IsPressed() && isGrounded)
        {
            jumpVelocity = jumpForce;
        }
        if (!isGrounded)
        {
            jumpVelocity -= gravity * Time.deltaTime;
        }
        else if (isGrounded && jumpVelocity < 0)
        {
            jumpVelocity = 0f;
        }

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime * jumpSpeed;
        if (HardCorrectPosition(movement)) {
            movement.y = 0;
        }
        Vector3 newPosition = transform.position + movement;
        transform.position = newPosition;

        if (movement.x != 0 || movement.z != 0)
        {
            Quaternion toRotate = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }


    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return hit.distance <= 0.1f;
        }
        
        return false;
    }

    bool HardCorrectPosition(Vector3 movement)
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return false;
        }

        if (hit.distance < -jumpVelocity * Time.deltaTime * 5)
        {
            Vector3 newPosition = transform.position + movement;
            transform.position = new Vector3(newPosition.x, hit.point.y + 0.01f, newPosition.z);
            return true;
        }
        return false;
    }
}
