using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceTesting : MonoBehaviour
{
    public Material originalMaterial; // The original material of the circle
    public Material hitMaterial; // The material to switch to when hit in the correct order
    public int orderIndex; // The order index for this circle

    public static List<SequenceTesting> circles = new List<SequenceTesting>();
    private static int currentOrderIndex = 0;
    private Renderer circleRenderer;

    public UnityEvent onComplete;
    public bool hit = false;

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

    private void Update()
    {
        if (orderIndex == 5 && currentOrderIndex > 5)
        {
            currentOrderIndex = 0;
            onComplete.Invoke();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hit) return;
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
                    hit = true;
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
                        circle.hit = false;
                    }
                }
                currentOrderIndex = 0;

                Debug.Log(currentOrderIndex);
            }
        }
    }
}
