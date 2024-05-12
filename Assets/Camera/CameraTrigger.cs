using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraTrigger : MonoBehaviour
{
    public GameObject cameraGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the Cinemachine camera GameObject
            cameraGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate the Cinemachine camera GameObject
            cameraGameObject.SetActive(false);
        }
    }
}
