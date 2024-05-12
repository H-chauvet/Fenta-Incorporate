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

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Limit the movement within the specified bounds
        newPosition.x = Mathf.Clamp(newPosition.x, xMin + xMinOffset, xMax - xMaxOffset);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin + zMinOffset, zMax - zMaxOffset);

        transform.position = newPosition;
    }
}
