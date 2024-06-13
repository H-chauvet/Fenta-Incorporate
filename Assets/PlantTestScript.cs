using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTestScript : MonoBehaviour
{
    public GameObject objectToEnable; // Assign the GameObject to enable in the Inspector

    private bool canEnable = false;

    void Update()
    {
        if (canEnable && Input.GetKeyDown(KeyCode.E))
        {
            EnableObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the enable trigger
        if (other.gameObject.CompareTag("EnableTrigger"))
        {
            canEnable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the enable trigger
        if (other.gameObject.CompareTag("EnableTrigger"))
        {
            canEnable = false;
        }
    }

    private void EnableObject()
    {
        // Check if the object to enable is set
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to enable is not set.");
        }
    }
}
