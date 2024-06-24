using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeSlide : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints the player will slide through
    public float slideSpeed = 2f;

    private int currentWaypointIndex = 0;
    private bool isSliding = false;
    private WardrobePlayerControls playerControls;

    void Start()
    {
        playerControls = GetComponent<WardrobePlayerControls>();
    }

    void Update()
    {
        if (isSliding && waypoints.Count > 0)
        {
            SlideToNextWaypoint();
        }
    }

    public void StartSliding()
    {
        if (waypoints.Count > 0)
        {
            isSliding = true;
            playerControls.SetSliding(true);
        }
    }

    void SlideToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            float step = slideSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, step);

            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.001f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            isSliding = false;
            playerControls.SetSliding(false);
            currentWaypointIndex = 0; // Reset index for next interaction
        }
    }
}
