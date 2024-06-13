using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSwitchScene : MonoBehaviour
{
    public string sceneName; // Assign the name of the scene to switch to in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the scene switch trigger
        if (other.gameObject.CompareTag("SceneSwitchTrigger"))
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        // Check if the scene name is set
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }
}
