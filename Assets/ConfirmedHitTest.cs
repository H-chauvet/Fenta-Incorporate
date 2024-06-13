using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmedHitTest : MonoBehaviour
{
    public Material originalMaterial; // The original material of the plane
    public Material newMaterial; // The material to switch to when the player steps on the plane

    private Renderer planeRenderer;

    void Start()
    {
        // Get the Renderer component of the plane
        planeRenderer = GetComponent<Renderer>();

        // Set the original material at the start
        if (planeRenderer != null && originalMaterial != null)
        {
            planeRenderer.material = originalMaterial;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("IOnCollisionEnter!");
        // Check if the collider is the ring
        if (collision.gameObject.CompareTag("Ring"))
        {
            Debug.Log("I have collided!!");
            // Switch to the new material
            if (planeRenderer != null && newMaterial != null)
            {
                planeRenderer.material = newMaterial;
            }
        }
    }

}
