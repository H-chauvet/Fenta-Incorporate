using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Material originalMaterial; // The original material of the plane
    public Material newMaterial; // The material to switch to when the player steps on the plane
    public GameObject objectToLaunch; // The GameObject to be launched
    public Vector3 launchForce = new Vector3(0, 10, 0); // The force with which to launch the object
    public Vector3 launchRotation = Vector3.zero; // The rotation to apply to the object when launched
    public float spinSpeed = 100f; // The speed at which the object spins on its X-axis

    private Renderer planeRenderer;
    private bool playerOnPlane = false; // To check if the player is on the plane
    private bool isObjectLaunched = false; // To check if the object is launched
    private Rigidbody rb; // The Rigidbody component of the object to launch

    void Start()
    {
        // Get the Renderer component of the plane
        planeRenderer = GetComponent<Renderer>();

        // Set the original material at the start
        if (planeRenderer != null && originalMaterial != null)
        {
            planeRenderer.material = originalMaterial;
        }

        // Ensure the object to launch has a Rigidbody component
        if (objectToLaunch != null)
        {
            rb = objectToLaunch.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Check if the player is on the plane and the "E" key is pressed
        if (playerOnPlane && Input.GetKeyDown(KeyCode.E))
        {
            LaunchObject();
        }
    }

    void FixedUpdate()
    {
        // Apply continuous spin on the X-axis if the object is launched
        if (isObjectLaunched && rb != null)
        {
            rb.AddTorque(Vector3.right * spinSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            playerOnPlane = true;

            // Switch to the new material
            if (planeRenderer != null && newMaterial != null)
            {
                planeRenderer.material = newMaterial;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            playerOnPlane = false;

            // Switch back to the original material
            if (planeRenderer != null && originalMaterial != null)
            {
                planeRenderer.material = originalMaterial;
            }
        }
    }

    void LaunchObject()
    {
        if (objectToLaunch != null && rb != null)
        {
            // Apply the launch force
            rb.AddForce(launchForce, ForceMode.Impulse);
            // Apply the rotation
            rb.rotation = Quaternion.Euler(launchRotation);
            // Mark the object as launched
            isObjectLaunched = true;
        }
    }
}
