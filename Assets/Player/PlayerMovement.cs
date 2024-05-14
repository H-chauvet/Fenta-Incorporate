using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject limitObject; // Object representing the limit
    public float xMinOffset = 0f;
    public float xMaxOffset = 0f;
    public float zMinOffset = 0f;
    public float zMaxOffset = 0f;

    private float xMin, xMax, zMin, zMax; // Bounds for x and z positions

    public float jumpForce = 10f;
    public float jumpSpeed = 5f;
    public float gravity = 9.8f;
    private float jumpVelocity = 0f;


    private void Start()
    {
        // Get the bounds from the limitObject
        Renderer renderer = limitObject.GetComponent<Renderer>();
        xMin = renderer.bounds.min.x;
        xMax = renderer.bounds.max.x;
        zMin = renderer.bounds.min.z;
        zMax = renderer.bounds.max.z;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpVelocity = jumpForce;
        }
        if (!isGrounded)
        {
            jumpVelocity -= gravity * Time.deltaTime;
        }
        else if (isGrounded && jumpVelocity < 0)
        {
            jumpVelocity = 0;
        }

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime * jumpSpeed;
        if (HardCorrectPosition(movement)) {
            movement.y = 0;
        }
        Vector3 newPosition = transform.position + movement;

        // Limit the movement within the specified bounds
        newPosition.x = Mathf.Clamp(newPosition.x, xMin + xMinOffset, xMax - xMaxOffset);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin + zMinOffset, zMax - zMaxOffset);
        transform.position = newPosition;
        CorrectPosition();
    }

    bool IsGrounded()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            return false;
        }
        float height = renderer.bounds.extents.y;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return hit.distance <= height + 0.1f;
        }
        return false;
    }

    void CorrectPosition()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            return;
        }
        float height = renderer.bounds.extents.y;
        
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return;
        }
        if (hit.distance <= height)
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            return;
        }
    }

    bool HardCorrectPosition(Vector3 movement)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            return false;
        }
        float height = renderer.bounds.extents.y;

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return false;
        }

        if (hit.distance - height / 2 < -jumpVelocity * Time.deltaTime * 5)
        {
            Vector3 newPosition = transform.position + movement;
            transform.position = new Vector3(newPosition.x, hit.point.y + height, newPosition.z);
            return true;
        }
        return false;
    }
}
