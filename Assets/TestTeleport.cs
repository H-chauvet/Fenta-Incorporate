using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeleport : MonoBehaviour
{
    public GameObject targetLocation; // Assign the target location in the Inspector

    private bool canTeleport = false;

    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            Teleport();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the teleport trigger
        if (other.gameObject.CompareTag("TeleportTrigger"))
        {
            Debug.Log("I have collided bro");
            canTeleport = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the teleport trigger
        if (other.gameObject.CompareTag("TeleportTrigger"))
        {
            canTeleport = false;
        }
    }

    private void Teleport()
    {
        // Check if the target location is set
        if (targetLocation != null)
        {
            transform.position = targetLocation.transform.position;
        }
        else
        {
            Debug.LogWarning("Target location is not set.");
        }
    }
}
