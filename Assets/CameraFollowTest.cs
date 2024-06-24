using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTest : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float xOffset = 0f; // Offset on the x-axis
    public float yOffset = 0f; // Offset on the y-axis
    public float zOffset = -10f; // Offset on the z-axis to position the camera behind the player

    void Update()
    {
        if (player != null)
        {
            // Update the camera's position to follow the player's x position with an x offset
            Vector3 newPosition = new Vector3(player.position.x + xOffset, player.position.y + yOffset, player.position.z + zOffset);
            transform.position = newPosition;
        }
    }
}
