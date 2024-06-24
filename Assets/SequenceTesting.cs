using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTesting : MonoBehaviour
{
    public Material originalMaterial; // The original material of the circle
    public Material hitMaterial; // The material to switch to when hit in the correct order
    public int orderIndex; // The order index for this circle

    public static List<SequenceTesting> circles = new List<SequenceTesting>();
    private static int currentOrderIndex = 0;
    private Renderer circleRenderer;

    void Start()
    {
        // Add this circle to the list
        circles.Add(this);

        // Get the Renderer component of the circle
        circleRenderer = GetComponent<Renderer>();

        // Set the original material at the start
        if (circleRenderer != null && originalMaterial != null)
        {
            circleRenderer.material = originalMaterial;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AYY");
        // Check if the collider is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if this circle is the correct one in the order
            if (orderIndex == currentOrderIndex)
            {
                // Switch to the hit material
                if (circleRenderer != null && hitMaterial != null)
                {
                    circleRenderer.material = hitMaterial;
                }
                currentOrderIndex++;
            }
            else
            {
                // Reset all circles to the original material
                foreach (SequenceTesting circle in circles)
                {
                    if (circle.circleRenderer != null && circle.originalMaterial != null)
                    {
                        circle.circleRenderer.material = circle.originalMaterial;
                    }
                }
                currentOrderIndex = 0;

                Debug.Log(currentOrderIndex);
            }
        }
    }
}
