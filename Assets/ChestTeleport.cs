using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTeleport : MonoBehaviour
{
    public Material originalMaterial; // The original material of the object
    public Material newMaterial; // The material to switch to upon trigger
    public GameObject teleportTarget; // The empty GameObject that defines the teleport location
    public Transform playerTransform; // Reference to the player's Transform

    private Renderer objectRenderer;
    private bool playerInTrigger = false;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Set the original material at the start
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }
    }

    void Update()
    {
        // Check if the player is in the trigger zone and the "E" key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;

            // Switch to the new material
            if (objectRenderer != null && newMaterial != null)
            {
                objectRenderer.material = newMaterial;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;

            // Switch back to the original material
            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
        }
    }

    void TeleportPlayer()
    {
        if (teleportTarget != null && playerTransform != null)
        {
            playerTransform.position = teleportTarget.transform.position;
        }
    }
}
